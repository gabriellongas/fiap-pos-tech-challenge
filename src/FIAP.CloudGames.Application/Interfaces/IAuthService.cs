using FIAP.CloudGames.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.CloudGames.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginUserDto dto);
    }
}
