using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Interfaces;
using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Interfaces.Repositories;

namespace FIAP.CloudGames.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Game> CreateAsync(CreateGameDto dto)
    {
        var game = new Game(
            dto.Title,
            dto.Price,
            dto.Description,
            dto.ReleaseDate,
            dto.Developer,
            dto.Publisher
        );

        await _gameRepository.AddAsync(game);
        return game;
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _gameRepository.GetAllAsync();
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await _gameRepository.GetByIdAsync(id);
    }

    public async Task<Game?> UpdateAsync(UpdateGameDto dto)
    {
        var game = await _gameRepository.GetByIdAsync(dto.Id);
        if (game == null) return null;

        game.Title = dto.Title;
        game.Price = dto.Price;
        game.Description = dto.Description;
        game.ReleaseDate = dto.ReleaseDate;
        game.Developer = dto.Developer;
        game.Publisher = dto.Publisher;

        return await _gameRepository.UpdateAsync(game);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null) return false;

        await _gameRepository.DeleteAsync(game.Id);
        return true;
    }
}
