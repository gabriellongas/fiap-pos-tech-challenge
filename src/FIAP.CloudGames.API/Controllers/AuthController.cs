using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.CloudGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }
    }
}
