using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Services;
using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.Interfaces;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.CloudGames.Tests.Application.UseCases.Games
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _authService = new AuthService(_userRepositoryMock.Object, _jwtTokenGeneratorMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var dto = new LoginUserDto
            {
                Email = "john@example.com",
                Password = "Password@123"
            };

            var user = new User(
                "John Doe",
                Email.Create(dto.Email),
                Password.FromPlainText(dto.Password),
                UserRoles.User
            );

            var expectedToken = "fake-jwt-token";

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            _jwtTokenGeneratorMock
                .Setup(j => j.GenerateToken(user.Id.ToString(), user.Email, user.Role.ToString()))
                .Returns(expectedToken);

            var result = await _authService.LoginAsync(dto);

            Assert.Equal(expectedToken, result);
            _jwtTokenGeneratorMock.Verify(j => j.GenerateToken(user.Id.ToString(), user.Email, user.Role.ToString()), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowUnauthorizedAccessException_WhenUserNotFound()
        {
            var dto = new LoginUserDto
            {
                Email = "invalid@example.com",
                Password = "Password!123"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(dto));
            _jwtTokenGeneratorMock.Verify(j => j.GenerateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowUnauthorizedAccessException_WhenPasswordIsInvalid()
        {
            var dto = new LoginUserDto
            {
                Email = "john@example.com",
                Password = "Password!123"
            };

            var user = new User(
                "John Doe",
                Email.Create(dto.Email),
                Password.FromPlainText("Password123@"), 
                UserRoles.Admin
            );

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(dto));
            _jwtTokenGeneratorMock.Verify(j => j.GenerateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }

}
