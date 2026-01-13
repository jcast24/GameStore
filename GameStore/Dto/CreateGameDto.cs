using System.ComponentModel.DataAnnotations;

namespace GameStore.Dto;

public record CreateGameDto(
    [Required] string Name,
    string GameGenre,
    decimal Price,
    DateOnly ReleaseDate
);
