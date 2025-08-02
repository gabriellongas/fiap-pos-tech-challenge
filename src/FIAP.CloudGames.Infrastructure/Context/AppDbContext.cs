using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FIAP.CloudGames.Infrastructure.Context;

public class AppDbContext: DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    DbSet<User> Users { get; set; }
    DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .HasConversion(
                v => v.Hash,
                v => Password.FromHashed(v)
            );

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasConversion(
                v => v.Address,
                v => Email.Create(v)
            );
    }
}
