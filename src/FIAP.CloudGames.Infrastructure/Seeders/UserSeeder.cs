using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;
using FIAP.CloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.CloudGames.Infrastructure.Seeders;

public static class UserSeeder
{
    public static async Task SeedAdminAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync(u => u.Role == UserRoles.Admin))
            return;

        var adminUser = new User(
            name: "Admin",
            email: Email.Create("admin@cloudgames.com"),
            password: Password.FromPlainText("Admin@1234!"),
            role: UserRoles.Admin
        );

        context.Users.Add(adminUser);
        await context.SaveChangesAsync();
    }
}
