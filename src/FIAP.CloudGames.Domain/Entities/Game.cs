namespace FIAP.CloudGames.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required decimal Price { get; set; }

    public string Description { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string Developer { get; set; }

    public string Publisher { get; set; }

    public Game(string title, decimal price, string description, DateTime releaseDate, string developer, string publisher)
    {
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
        Description = description;
        ReleaseDate = releaseDate;
        Developer = developer;
        Publisher = publisher;
    }

    private Game() {  }
}
