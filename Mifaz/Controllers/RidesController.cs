using Microsoft.AspNetCore.Mvc;
using Mifaz.ApiModels;
using Mifaz.Authorization;
using Mifaz.Services;

namespace Mifaz.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RidesController : ControllerBase
{
    private readonly IRideService _rideService;

    public RidesController(IRideService rideService)
    {
        _rideService = rideService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRide([FromBody] CreateRideRequest request, CancellationToken token)
    {
        var ride = await _rideService.CreateRide(request.DriverId, request.Price, request.Date,
            request.Origin, request.Destination, token);
        return Ok(ride);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var rides = await _rideService.GetAll(token);
        return Ok(rides);
    }
}