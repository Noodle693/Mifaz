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
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthUserRequest request, CancellationToken token)
    {
        var user = await _userService.Authenticate(request, token);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var users = await _userService.GetAll(token);
        return Ok(users);
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken token)
    {
        var user = await _userService.CreateUser(request, token);
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("reset")]
    public async Task<IActionResult> Reset([FromBody] ResetPasswordRequest request, CancellationToken token)
    {
        var result = await _userService.Reset(request, token);
        return Ok(result);
    }
}