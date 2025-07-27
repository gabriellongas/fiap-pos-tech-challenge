using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required Email Email { get; set; }

    public required string PasswordHash { get; set; }

    public required string Role { get; set; }

    public DateTime CreatedAt { get; set; }

    User()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
