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

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var rides = await _rideService.GetAll(token);
        return Ok(rides);
    }

    [HttpGet("{personId:int}")]
    public async Task<IActionResult> GetPersonRides([FromRoute] int personId, [FromQuery] bool isDriver,
        CancellationToken token)
    {
        var request = new GetRidesRequest { PersonId = personId, IsDriver = isDriver };
        var rides = await _rideService.GetAllForPerson(request, token);
        return Ok(rides);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRide([FromBody] CreateRideRequest request, CancellationToken token)
    {
        var ride = await _rideService.CreateRide(request, token);
        return Ok(ride);
    }
}