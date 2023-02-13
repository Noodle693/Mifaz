using Microsoft.AspNetCore.Mvc;
using Mifaz.ApiModels;
using Mifaz.Authorization;
using Mifaz.Services;

namespace Mifaz.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RideUserAssociationsController : ControllerBase
{
    private readonly IRideUserAssociationService _rideUserAssociationService;

    public RideUserAssociationsController(IRideUserAssociationService rideUserAssociationService)
    {
        _rideUserAssociationService = rideUserAssociationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRideUserAssociation([FromBody] CreateRideUserAssociationRequest request,
        CancellationToken token)
    {
        var rideUserAssociation =
            await _rideUserAssociationService.CreateRideUserAssociation(request.RideId, request.PassengerId, token);
        return Ok(rideUserAssociation);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var rideUserAssociations = await _rideUserAssociationService.GetAll(token);
        return Ok(rideUserAssociations);
    }
}