using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Email Email { get; set; }

    public Password Password { get; set; }

    public UserRoles Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public User(string name, Email email, Password password, UserRoles role)
    {
        Id = Guid.NewGuid();
        CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    private User() {  }
}
