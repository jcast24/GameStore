namespace GameStore.Models;

public class Game
{
    public int Id { get; set; }

    // adding required so that we provide a value
    public required string Name { get; set; }

    public Genre? Genre { get; set; }

    public int GenreId { get; set; }
}
