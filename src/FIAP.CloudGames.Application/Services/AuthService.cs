using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Interfaces;
using FIAP.CloudGames.Domain.Interfaces;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.CloudGames.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> LoginAsync(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !user.Password.Verify(dto.Password))
                throw new UnauthorizedAccessException("Credenciais inválidas");

            return _jwtTokenGenerator.GenerateToken(user.Id.ToString(), user.Email, user.Role.ToString());
        }
    }
}
