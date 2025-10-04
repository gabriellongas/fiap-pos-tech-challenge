using System;

namespace FIAP.CloudGames.Application.Dtos;

public class CreateGameDto
{
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Developer { get; set; }
    public string? Publisher { get; set; }
}
