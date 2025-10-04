using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Interfaces;
using FIAP.CloudGames.Application.Services;
using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace FIAP.CloudGames.Tests.Application.UseCases.Games;

public class GameServiceTests
{
    [Fact]
    public async Task CreateGame_ShouldAddGameToRepository()
    {
        var service = CreateService(out var mockRepo);
        var request = new CreateGameDto
        {
            Title = "The Witcher 3",
            Price = 129.99m,
            Description = "RPG",
            ReleaseDate = new DateTime(2015, 5, 18),
            Developer = "CD Projekt Red",
            Publisher = "CD Projekt Red"
        };

        var result = await service.CreateAsync(request);

        result.Should().NotBeNull();
        result.Title.Should().Be("The Witcher 3");

        mockRepo.Verify(x => x.AddAsync(It.IsAny<Game>()), Times.Once);
    }

    [Fact]
    public async Task GetGameById_ShouldReturnGame_WhenExists()
    {
        var service = CreateService(out var mockRepo);
        var fakeGame = new Game("Hades", 73.99m, "Rogue-like", new DateTime(2020, 9, 17), "Supergiant Games", "Supergiant Games");
        mockRepo.Setup(x => x.GetByIdAsync(fakeGame.Id)).ReturnsAsync(fakeGame);

        var result = await service.GetByIdAsync(fakeGame.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(fakeGame.Id);
    }

    [Fact]
    public async Task GetGameById_ShouldReturnNull_WhenNotExists()
    {
        var service = CreateService(out var mockRepo);
        mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Game)null!);

        var result = await service.GetByIdAsync(Guid.NewGuid());

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateGame_ShouldModifyGameProperties()
    {
        var service = CreateService(out var mockRepo);
        var game = new Game("Old Title", 29.99m, "Desc", DateTime.Now, "Dev", "Pub");
        mockRepo.Setup(x => x.GetByIdAsync(game.Id)).ReturnsAsync(game);
        mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Game>())).ReturnsAsync(game);

        var dto = new UpdateGameDto
        {
            Id = game.Id,
            Title = "New Title",
            Price = 59.99m,
            Description = "Updated",
            ReleaseDate = game.ReleaseDate,
            Developer = game.Developer,
            Publisher = game.Publisher
        };

        var result = await service.UpdateAsync(dto);

        result.Title.Should().Be("New Title");
        result.Price.Should().Be(59.99m);
    }

    [Fact]
    public async Task DeleteGame_ShouldRemoveGame()
    {
        var service = CreateService(out var mockRepo);
        var game = new Game("To Delete", 10.00m, "Some", DateTime.Now, "Dev", "Pub");

        mockRepo.Setup(x => x.GetByIdAsync(game.Id)).ReturnsAsync(game);
        mockRepo.Setup(x => x.DeleteAsync(game.Id)).Returns(Task.FromResult(true));

        var result = await service.DeleteAsync(game.Id);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task GetAllGames_ShouldReturnAllGames()
    {
        var service = CreateService(out var mockRepo);

        var games = new List<Game>
        {
            new("Game 1", 20, "One", DateTime.Now, "Dev1", "Pub1"),
            new("Game 2", 30, "Two", DateTime.Now, "Dev2", "Pub2")
        };

        mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(games);

        var result = await service.GetAllAsync();

        result.Should().HaveCount(2);
    }

    private IGameService CreateService(out Mock<IGameRepository> mockRepo)
    {
        mockRepo = new Mock<IGameRepository>();
        return new GameService(mockRepo.Object);
    }
}
