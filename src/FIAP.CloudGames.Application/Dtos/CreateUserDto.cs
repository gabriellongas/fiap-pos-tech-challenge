using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.CloudGames.Application.Dtos
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public Email Email { get; set; }
        public string Password { get; set; }
        public UserRoles Role { get; set; }
    }
}
