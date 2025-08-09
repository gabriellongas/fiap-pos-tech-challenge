using FIAP.CloudGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.CloudGames.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllWithLibraryAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> UpdateAsync(User user);
        // NOVO: inclui a biblioteca
        // Issue#17
        Task<User?> GetByIdWithLibraryAsync(Guid id);

        Task DeleteAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
    }
}
