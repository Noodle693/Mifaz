using Mifaz.Database;
using Mifaz.Models;

namespace Mifaz.Services;

public interface IRideUserAssociationService
{
    Task<RideUserAssociation> CreateRideUserAssociation(int rideId, int passengerId,
        CancellationToken token);

    Task<IEnumerable<RideUserAssociation>> GetAll(CancellationToken token);
}

public class RideUserAssociationService : IRideUserAssociationService
{
    private readonly MifazDbContext _context;

    public RideUserAssociationService(MifazDbContext context)
    {
        _context = context;
    }

    public async Task<RideUserAssociation> CreateRideUserAssociation(int rideId, int passengerId,
        CancellationToken token)
    {
        return await _context.CreateRideUserAssociation(rideId, passengerId, token);
    }

    public async Task<IEnumerable<RideUserAssociation>> GetAll(CancellationToken token)
    {
        return await _context.GetRideUserAssociations(token);
    }
}