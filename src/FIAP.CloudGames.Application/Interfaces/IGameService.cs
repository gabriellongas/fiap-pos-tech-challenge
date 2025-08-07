using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Domain.Entities;

namespace FIAP.CloudGames.Application.Interfaces;

public interface IGameService
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(Guid id);
    Task<Game> CreateAsync(CreateGameDto dto);
    Task<Game?> UpdateAsync(UpdateGameDto dto);
    Task<bool> DeleteAsync(Guid id);
}
