using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.CloudGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("GetById solicitado {UserId}", id);

            var user = await _userService.GetByIdAsync(id);

            if (user is null)
            {
                _logger.LogWarning("User não encontrado {UserId}", id);
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            _logger.LogInformation("Create user solicitado {Email}", dto.Email);

            var createdUser = await _userService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            _logger.LogInformation("Update user solicitado {UserId}", dto.Id);

            var updatedUser = await _userService.UpdateAsync(dto);
            if (updatedUser is null)
            {
                _logger.LogWarning("Update falhou, user não encontrado {UserId}", dto.Id);
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Delete user solicitado {UserId}", id);

            var deletedUser = await _userService.DeleteAsync(id);

            if (!deletedUser)
            {
                _logger.LogWarning("Delete falhou, user não encontrado {UserId}", id);
                return NotFound();
            }

            _logger.LogInformation("User deletado com sucesso {UserId}", id);
            return NoContent();
        }

    }
}
