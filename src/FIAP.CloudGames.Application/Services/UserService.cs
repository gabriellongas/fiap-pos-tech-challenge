using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Interfaces;
using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(CreateUserDto dto)
        {
            var password = Password.FromPlainText(dto.Password);
            var email = Email.Create(dto.Email);

            var user = new User(
                dto.Name,
                email,
                password,
                dto.Role
            );

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }


        public async Task<User?> UpdateAsync(UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.Id);
            if (user == null) return null;

            var password = Password.FromPlainText(dto.Password);
            var email = Email.Create(dto.Email);

            user.Name = dto.Name;
            user.Email = email;
            user.Password = password;
            user.Role = dto.Role;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user.Id);
            return true;
        }
    }
}
