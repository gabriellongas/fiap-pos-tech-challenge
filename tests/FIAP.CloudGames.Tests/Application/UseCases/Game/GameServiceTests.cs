using FIAP.CloudGames.Application.Dtos;
using FIAP.CloudGames.Application.Interfaces;
using FluentAssertions;

namespace FIAP.CloudGames.Tests.Application.UseCases.Game
{
    public class GameServiceTests
    {
        [Fact]
        public async Task CreateGame_ShouldAddGameToRepository()
        {
            var service = CreateService();
            var request = new CreateGameDto()
            {
                Title = "The Witcher 3",
                Price = 129.99m,
                Description = "RPG",
                ReleaseDate = new DateTime(2015, 5, 18),
                Developer = "CD Projekt Red",
                Publisher = "CD Projekt Red"
            };

            var result = await service.CreateGameAsync(request);

            result.Should().NotBeNull();
            result.Title.Should().Be("The Witcher 3");
        }

        [Fact]
        public async Task GetGameById_ShouldReturnGame_WhenExists()
        {
            var service = CreateService();
            var created = await service.CreateGameAsync(new CreateGameDto
            {
                Title = "Hades",
                Price = 73.99m,
                Description = "Rogue-like",
                ReleaseDate = new DateTime(2020, 9, 17),
                Developer = "Supergiant Games",
                Publisher = "Supergiant Games"
            });

            var result = await service.GetGameByIdAsync(created.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(created.Id);
        }

        [Fact]
        public async Task GetGameById_ShouldReturnNull_WhenNotExists()
        {
            var service = CreateService();

            var result = await service.GetGameByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateGame_ShouldModifyGameProperties()
        {
            var service = CreateService();
            var created = await service.CreateGameAsync(new CreateGameDto() 
            {
                Title = "Old Title",
                Price = 29.99m,
                Description = "Desc",
                ReleaseDate = DateTime.Now,
                Developer = "Dev",
                Publisher = "Pub"
            });

            var update = new UpdateGameDto() 
            {
                Id = created.Id,
                Title = "New Title",
                Price = 59.99m,
                Description = "Updated",
                ReleaseDate = created.ReleaseDate,
                Developer = created.Developer,
                Publisher = created.Publisher
            };

            var result = await service.UpdateGameAsync(update);

            result.Title.Should().Be("New Title");
            result.Price.Should().Be(59.99m);
        }

        [Fact]
        public async Task DeleteGame_ShouldRemoveGame()
        {
            var service = CreateService();
            var created = await service.CreateGameAsync(new CreateGameDto() 
            {
                Title = "To Delete",
                Price = 10.00m,
                Description = "Some",
                ReleaseDate = DateTime.Now,
                Developer = "Dev",
                Publisher = "Pub"
            });

            await service.DeleteGameAsync(created.Id);

            var result = await service.GetGameByIdAsync(created.Id);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllGames_ShouldReturnAllGames()
        {
            var service = CreateService();
            await service.CreateGameAsync(new CreateGameDto() 
            {
                Title = "Game 1",
                Price = 20,
                Description = "One",
                ReleaseDate = DateTime.Now,
                Developer = "Dev1",
                Publisher = "Pub1"
            });
            await service.CreateGameAsync(new CreateGameDto() 
            { 
                Title = "Game 2", 
                Price = 30, 
                Description = "Two", 
                ReleaseDate = DateTime.Now, 
                Developer = "Dev2", 
                Publisher = "Pub2" 
            });

            var games = await service.GetAllGamesAsync();

            games.Should().HaveCount(2);
        }

        private IGameService CreateService()
        {
            throw new NotImplementedException("Implementar");
        }
    }
}
