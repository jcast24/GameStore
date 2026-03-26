using GameStore.Data;
using GameStore.Dto;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    // Don't hardcode strings, always use const variables for multiple use of strings
    private const string GetGameEndpointName = "GetGame";

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // group.MapGet("/", () => games);

        group.MapGet("/",
            async (GameStoreContext dbContext) => await dbContext.Games.Include(game => game.Genre)
                .Select(game => new GameSummaryDto(game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate))
                .AsNoTracking()
                .ToListAsync());


        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);

            // var game = games.Find(game => game.Id == id);

            return game is null
                ? Results.NotFound()
                : Results.Ok(new GameDetailsDto(
                    game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate));
        }).WithName(GetGameEndpointName);

        // Post converted using dbContext to store games into db
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            // GameDto game = new(
            //     games.Count + 1,
            //     newGame.Name,
            //     newGame.GameGenre,
            //     newGame.Price,
            //     newGame.ReleaseDate
            //         );

            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            // 201 response, game has been Created
            // return payload of the game created
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
        });

        // first parameter comes directly from the route 
        // while the second comes from the body of the request
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            // locate the "thing" or this case "game", that we need to update
            // var index = games.FindIndex(game => game.Id == id);
            var existingGame = await dbContext.Games.FindAsync(id);

            // if the game isn't found, return 404 Not Found (?)
            // FindIndex() returns -1 if not found
            if (existingGame is null) return Results.NotFound();

            // update the element based on the index
            // games[index] = new GameDto(id,
            //     updatedGame.Name,
            //     updatedGame.Genre,
            //     updatedGame.Price,
            //     updatedGame.ReleaseDate);

            existingGame.Name = updatedGame.Name;
            existingGame.GenreId = updatedGame.GenreId;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            // bulk deletion
            // efficient, only 1 call to the database 
            // no need to run the SaveChangesAsync() method
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });
    }
}