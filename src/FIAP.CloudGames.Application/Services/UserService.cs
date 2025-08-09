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
        private readonly IGameRepository _gameRepository;

        public UserService(IUserRepository userRepository, IGameRepository gameRepository) 
        {
            _userRepository = userRepository;
            _gameRepository = gameRepository;
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
            return await _userRepository.GetAllWithLibraryAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdWithLibraryAsync(id);
        }


        public async Task<User?> UpdateAsync(UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.Id);
            if (user == null) return null;

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Password = dto.Password;
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

        public async Task PurchaseGameAsync(Guid userId, Guid gameId)
        {
            var user = await _userRepository.GetByIdWithLibraryAsync(userId)
                       ?? await _userRepository.GetByIdAsync(userId);
            var game = await _gameRepository.GetByIdAsync(gameId);

            if (user is null || game is null)
                throw new InvalidOperationException("Usuário ou jogo não encontrado.");

            user.Library ??= new List<Game>();

            if (user.Library.Any(g => g.Id == gameId))
                throw new InvalidOperationException("Jogo já está na biblioteca do usuário.");

            user.Library.Add(game);
            await _userRepository.UpdateAsync(user);
        }
    }
}
