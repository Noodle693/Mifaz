using Mifaz.Database;
using Mifaz.Models;

namespace Mifaz.Services;

public interface IRideService
{
    Task<Ride> CreateRide(int driverId, double price, DateTime startingDate,
        DateTime endingDate, string startingCity, string destinationCity, CancellationToken token);

    Task<IEnumerable<Ride>> GetAll(CancellationToken token);
}

public class RideService : IRideService
{
    private readonly MifazDbContext _context;

    public RideService(MifazDbContext context)
    {
        _context = context;
    }

    public async Task<Ride> CreateRide(int driverId, double price, DateTime startingDate, DateTime endingDate,
        string startingCity,
        string destinationCity, CancellationToken token)
    {
        return await _context.CreateRide(driverId, price, startingDate, endingDate, startingCity, destinationCity,
            token);
    }

    public async Task<IEnumerable<Ride>> GetAll(CancellationToken token)
    {
        return await _context.GetRides(token);
    }
}