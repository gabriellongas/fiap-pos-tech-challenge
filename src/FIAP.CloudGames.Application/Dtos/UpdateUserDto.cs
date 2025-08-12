using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Application.Dtos
{
    public class UpdateUserDto
    {
        public required Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoles Role { get; set; }
    }
}
