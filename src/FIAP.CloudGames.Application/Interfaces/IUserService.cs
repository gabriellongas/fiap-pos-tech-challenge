using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Domain.Entities;

namespace FIAP.CloudGames.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> CreateAsync(CreateUserDto dto);
        Task<User?> UpdateAsync(UpdateUserDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task PurchaseGameAsync(Guid userId, Guid gameId);
    }
}
