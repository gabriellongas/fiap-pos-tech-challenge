using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.CloudGames.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetAll()
    {
        var games = await _gameService.GetAllAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var game = await _gameService.GetByIdAsync(id);
        return game == null ? NotFound() : Ok(game);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateGameDto dto)
    {
        var createdGame = await _gameService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdGame.Id }, createdGame);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateGameDto dto)
    {
        var updated = await _gameService.UpdateAsync(dto);
        return updated != null ? Ok(updated) : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _gameService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
