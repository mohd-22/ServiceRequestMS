using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
namespace ServiceRequestMS.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto request)
    {
        var token = await _authService.LoginAsync(request);
        if (!token.Success)
        {
            return BadRequest(token);
        }
        return Ok(token.Data);
    }
}
