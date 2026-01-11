namespace GameStore.Dto;

public record CreateGameDto(
        string Name,
        string GameGenre,
        decimal Price,
        DateOnly ReleaseDate
        );
