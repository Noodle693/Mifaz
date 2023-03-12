using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Mifaz.ApiModels.ResponseModels;
using Mifaz.Models;
using Mifaz.Services;

namespace Mifaz.Database;

public class MifazDbContext : DbContext
{
    private readonly PasswordHashingService _passwordHashingService;

    public MifazDbContext(DbContextOptions<MifazDbContext> options, PasswordHashingService passwordHashingService) :
        base(options)
    {
        _passwordHashingService = passwordHashingService;
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Ride> Rides { get; set; }
    public virtual DbSet<RideUserAssociation> RideUserAssociations { get; set; }

    public async Task<User> GetUser(string mail, string password, CancellationToken token)
    {
        var user = await Users.FirstOrDefaultAsync(x => x.Mail == mail, token);
        if (_passwordHashingService.Verify(password, user!.Password))
            return user;
        throw new UnauthorizedAccessException();
    }

    public async Task<User?> GetDriver(int id, CancellationToken token)
    {
        var driver = await Users.FirstOrDefaultAsync(x => x.Id == id, token);
        return driver ?? null;
    }

    public async Task<IEnumerable<User>> GetUsers(CancellationToken token)
    {
        return await Users.ToListAsync(token);
    }

    public async Task<User> CreateUser(string username, string password, string firstName, string lastName,
        string phone, CancellationToken token)
    {
        if (await Users.FirstOrDefaultAsync(x => x.Mail == username, token) is not null)
            throw new ConstraintException("User already exists");
        var hashedPassword = _passwordHashingService.Hash(password);
        var user = new User
            { Mail = username, Password = hashedPassword, FirstName = firstName, LastName = lastName, Phone = phone };
        await Users.AddAsync(user, token);
        await SaveChangesAsync(token);
        return user;
    }

    public async Task<bool> ResetPassword(string mail, CancellationToken token)
    {
        var user = await Users.FirstOrDefaultAsync(user => user.Mail == mail, token);
        if (user is null) return false;
        user.Password = _passwordHashingService.Hash("default");
        Users.Update(user);
        await SaveChangesAsync(token);
        return true;
    }

    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    public async Task<IEnumerable<ResponseRide>> GetRides(int personId, bool isDriver, CancellationToken token)
    {
        if (personId == 0)
        {
            var rides = await Rides.ToListAsync(token);
            return await MapRidesToResponseRides(rides, token);
        }

        if (isDriver)
        {
            var rides = await Rides.Where(r => r.DriverId == personId).Select(r => r).ToListAsync(token);
            return await MapRidesToResponseRides(rides, token);
        }

        var associations = RideUserAssociations.Where(r => r.PassengerId == personId).Select(r => r);
        var tmpRides = new List<Ride>();
        foreach (var association in associations.ToList())
        {
            var tmp = await Rides.FirstOrDefaultAsync(ride => ride.Id == association.RideId, token);
            if (tmp is not null) tmpRides.Add(tmp);
        }
        return await MapRidesToResponseRides(tmpRides, token);
    }

    private async Task<List<ResponseRide>> MapRidesToResponseRides(IEnumerable<Ride> rides, CancellationToken token)
    {
        var response = new List<ResponseRide>();
        foreach (var ride in rides)
        {
            var driver = await GetDriver(ride.DriverId, token);
            var passengerCount = RideUserAssociations.Where(r => r.RideId == ride.Id).Select(r => r);
            response.Add(new ResponseRide
            {
                DriverFirstName = driver!.FirstName, DriverLastName = driver!.LastName, Origin = ride.Origin,
                Destination = ride.Destination, Date = ride.Date, PassengerCount = await passengerCount.CountAsync(token),
                MaxCount = ride.MaxCount, Cost = ride.Cost
            });
        }
        return response;
    }

    public async Task<Ride> CreateRide(int driverId, string origin, string destination, DateTime date,
        int passengerAmount, double price,
        CancellationToken token)
    {
        var ride = new Ride
        {
            DriverId = driverId,
            Origin = origin,
            Destination = destination,
            Date = date,
            MaxCount = passengerAmount,
            Cost = price
        };
        await Rides.AddAsync(ride, token);
        await SaveChangesAsync(token);
        return ride;
    }
}