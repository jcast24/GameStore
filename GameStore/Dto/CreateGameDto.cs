using System.ComponentModel.DataAnnotations;

namespace GameStore.Dto;

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(30)] string GameGenre,
    decimal Price,
    DateOnly ReleaseDate
);
