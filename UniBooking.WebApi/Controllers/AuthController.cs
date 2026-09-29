using Microsoft.AspNetCore.Mvc;
using UniBooking.Application.Features.Auth.Dtos;
using UniBooking.Application.Features.Auth;

namespace UniBooking.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequestDto request)
        => Ok(await _authService.RegisterAsync(request));

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto request)
    => Ok(await _authService.LoginAsync(request));
}