using Mifaz.ApiModels;
using Mifaz.Database;

namespace Mifaz.Services;

public interface IRideService
{
    Task<GetRidesResponse> GetAll(CancellationToken token);
    Task<GetRidesResponse> GetAllForPerson(GetRidesRequest request, CancellationToken token);
    Task<CreateRideResponse> CreateRide(CreateRideRequest request,
        CancellationToken token);
}

public class RideService : IRideService
{
    private readonly MifazDbContext _context;

    public RideService(MifazDbContext context)
    {
        _context = context;
    }

    public async Task<GetRidesResponse> GetAll(CancellationToken token)
    {
        return new GetRidesResponse((await _context.GetRides(0, false, token)).ToList());
    }

    public async Task<GetRidesResponse> GetAllForPerson(GetRidesRequest request, CancellationToken token)
    {
        return new GetRidesResponse((await _context.GetRides(request.PersonId, request.IsDriver, token)).ToList());
    }

    public async Task<CreateRideResponse> CreateRide(CreateRideRequest request, CancellationToken token)
    {
        return new CreateRideResponse(await _context.CreateRide(request.DriverId, request.Origin, request.Destination,
            request.Date, request.MaxCount,
            request.Cost, token));
    }
}