using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.CloudGames.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _context.Set<Game>().ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Game>().FindAsync(id);
    }

    public async Task AddAsync(Game game)
    {
        await _context.Set<Game>().AddAsync(game);
        await _context.SaveChangesAsync();
    }

    public async Task<Game> UpdateAsync(Game game)
    {
        _context.Set<Game>().Update(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task DeleteAsync(Guid id)
    {
        var game = await GetByIdAsync(id);
        if (game is null) return;

        _context.Set<Game>().Remove(game);
        await _context.SaveChangesAsync();
    }
}
