using GameStore.Dto;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    // Don't hardcode strings, always use const variables for multiple use of strings
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new GameDto(1, "Street Fighter", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new GameDto(2, "FIFA 99", "Sports", 19.99M, new DateOnly(1997, 11, 18)),
        new GameDto(3, "Warcraft 3", "Strategy", 15.99M, new DateOnly(2002, 7, 3)),
        new GameDto(4, "Star Wars: Knights of the Old Republic", "RPG", 18.99M, new DateOnly(2003, 7, 15)),
        new GameDto(5, "The Witcher", "Role-Playing Game", 17.99M, new DateOnly(2007, 10, 26)),
        new GameDto(6, "Counter Strike 1.6", "FPS", 0.00M, new DateOnly(2003, 09, 12)),
        new GameDto(7, "Jax and Daxter", "Action-Adventure", 19.99M, new DateOnly(2001, 12, 4)),
        new GameDto(8, "Sly-Cooper", "Action-Adventure", 19.99M, new DateOnly(2002, 9, 23)),
        new GameDto(9, "NBA Street Vol. 2", "Sports", 18.99M, new DateOnly(2003, 4, 28)),
        new GameDto(10, "Grand Theft Auto III", "Action-Adventure", 13.99M, new DateOnly(2001, 10, 22)),
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);

            if (game is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(game);

        }).WithName(GetGameEndpointName);

        group.MapPost("/addgame", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.GameGenre,
                newGame.Price,
                newGame.ReleaseDate
                    );

            games.Add(game);

            // 201 response, game has been Created
            // return payload of the game created
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // first parameter comes directly from the route 
        // while the second comes from the body of the request
        group.MapPut("/updategame/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            // locate the "thing" or this case "game", that we need to update
            var index = games.FindIndex(game => game.Id == id);

            // if the game isn't found, return 404 Not Found (?)
            // FindIndex() returns -1 if not found
            if (index == -1)
            {
                return Results.NotFound();
            }

            // update the element based on the index
            games[index] = new GameDto(id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate);

            return Results.NoContent();
        });

        group.MapDelete("/deletegame/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }

            games.Remove(game);
            return Results.NoContent();
        });
    }
}
