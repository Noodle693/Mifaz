using Microsoft.AspNetCore.Mvc;
using Mifaz.ApiModels;
using Mifaz.Authorization;
using Mifaz.Services;

namespace Mifaz.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken token)
    {
        var user = await _userService.CreateUser(request.Username, request.Password, request.Phone, token);
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthUserRequest request, CancellationToken token)
    {
        var user = await _userService.Authenticate(request.Username, request.Password, token);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var users = await _userService.GetAll(token);
        return Ok(users);
    }
}