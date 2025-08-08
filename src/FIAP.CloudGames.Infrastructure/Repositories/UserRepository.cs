using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FIAP.CloudGames.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Set<User>().Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user is null) return;

            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
