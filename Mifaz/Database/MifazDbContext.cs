using System.Data;
using Microsoft.EntityFrameworkCore;
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

    public async Task<User> CreateUser(string username, string password, string phone, CancellationToken token)
    {
        if (await Users.FirstOrDefaultAsync(x => x.Username == username, token) is not null)
            throw new ConstraintException("User already exists");
        var hashedPassword = _passwordHashingService.Hash(password);
        var user = new User { Username = username, Password = hashedPassword, Phone = phone };
        await Users.AddAsync(user, token);
        await SaveChangesAsync(token);
        return user;
    }

    public async Task<User> GetUser(string username, string password, CancellationToken token)
    {
        var user = await Users.FirstOrDefaultAsync(x => x.Username == username, token);
        if (_passwordHashingService.Verify(password, user!.Password))
            return user;
        throw new UnauthorizedAccessException();
    }

    public async Task<IEnumerable<User>> GetUsers(CancellationToken token)
    {
        return await Users.ToListAsync(token);
    }

    public async Task<Ride> CreateRide(int driverId, double price, DateTime startingDate,
        DateTime endingDate, string startingCity, string destinationCity, CancellationToken token)
    {
        var ride = new Ride
        {
            DriverId = driverId, Price = price, StartingDate = startingDate, EndingDate = endingDate,
            StartingCity = startingCity, DestinationCity = destinationCity
        };
        await Rides.AddAsync(ride, token);
        await SaveChangesAsync(token);
        return ride;
    }

    public async Task<IEnumerable<Ride>> GetRides(CancellationToken token)
    {
        return await Rides.ToListAsync(token);
    }

    public async Task<RideUserAssociation> CreateRideUserAssociation(int rideId, int passengerId,
        CancellationToken token)
    {
        var rideUserAssociation = new RideUserAssociation
        {
            RideId = rideId,
            PassengerId = passengerId
        };
        await RideUserAssociations.AddAsync(rideUserAssociation, token);
        await SaveChangesAsync(token);
        return rideUserAssociation;
    }

    public async Task<IEnumerable<RideUserAssociation>> GetRideUserAssociations(CancellationToken token)
    {
        return await RideUserAssociations.ToListAsync(token);
    }
}