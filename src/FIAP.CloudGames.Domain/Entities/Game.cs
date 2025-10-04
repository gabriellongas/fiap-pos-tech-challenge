using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIAP.CloudGames.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,6)")]
    public decimal Price { get; set; }

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
