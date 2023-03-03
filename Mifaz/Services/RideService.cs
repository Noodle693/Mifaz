using Mifaz.Database;
using Mifaz.Models;

namespace Mifaz.Services;

public interface IRideService
{
    Task<Ride> CreateRide(int driverId, double price, DateTime date, string origin, string destination,
        CancellationToken token);

    Task<IEnumerable<Ride>> GetAll(CancellationToken token);
}

public class RideService : IRideService
{
    private readonly MifazDbContext _context;

    public RideService(MifazDbContext context)
    {
        _context = context;
    }

    public async Task<Ride> CreateRide(int driverId, double price, DateTime date,
        string origin,
        string destination, CancellationToken token)
    {
        return await _context.CreateRide(driverId, price, date, origin, destination,
            token);
    }

    public async Task<IEnumerable<Ride>> GetAll(CancellationToken token)
    {
        return await _context.GetRides(token);
    }
}