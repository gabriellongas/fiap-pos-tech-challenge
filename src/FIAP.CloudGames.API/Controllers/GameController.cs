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
    private readonly ILogger<GameController> _logger;

    public GameController(IGameService gameService, ILogger<GameController> logger)
    {
        _gameService = gameService;
        _logger = logger;
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
        _logger.LogInformation("GetById game solicitado {GameId}", id);

        var game = await _gameService.GetByIdAsync(id);

        if (game is null)
        {
            _logger.LogWarning("Game não encontrado {GameId}", id);
            return NotFound();
        }

        return Ok(game);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateGameDto dto)
    {
        _logger.LogInformation("Create game solicitado {@GameDto}", dto);

        var createdGame = await _gameService.CreateAsync(dto);

        _logger.LogInformation("Game criado com sucesso {GameId}", createdGame.Id);
        return CreatedAtAction(nameof(GetById), new { id = createdGame.Id }, createdGame);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateGameDto dto)
    {
        _logger.LogInformation("Update game solicitado {GameId}", dto.Id);

        var updated = await _gameService.UpdateAsync(dto);

        if (updated is null)
        {
            _logger.LogWarning("Update falhou, game não encontrado {GameId}", dto.Id);
            return NotFound();
        }

        _logger.LogInformation("Game atualizado com sucesso {GameId}", dto.Id);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Delete game solicitado {GameId}", id);

        var deleted = await _gameService.DeleteAsync(id);

        if (!deleted)
        {
            _logger.LogWarning("Delete falhou, game não encontrado {GameId}", id);
            return NotFound();
        }

        _logger.LogInformation("Game deletado com sucesso {GameId}", id);
        return NoContent();
    }
}
