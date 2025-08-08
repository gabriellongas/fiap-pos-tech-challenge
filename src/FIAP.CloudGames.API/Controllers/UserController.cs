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

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            var user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var createdUser = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            var updatedUser = await _userService.UpdateAsync(dto);
            return updatedUser != null ? Ok(updatedUser) : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedUser = await _userService.DeleteAsync(id);
            return deletedUser ? NoContent() : NotFound();
        }
    }
}
