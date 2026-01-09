using GameStore.Dto;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new GameDto(1, "Street Fighter", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new GameDto(2, "FIFA 99", "Sports", 19.99M, new DateOnly(1997, 11, 18)),
    new GameDto(3, "Warcraft 3", "Strategy", 15.99M, new DateOnly(2002, 7, 3)),
    new GameDto(4, "Star Wars: Knights of the Old Republic", "RPG", 18.99M, new DateOnly(2003, 7, 15)),
    new GameDto(5, "The Witcher", "Role-Playing Game", 17.99M, new DateOnly(2007, 10, 26)),
    new GameDto(6, "Counter Strike 1.6", "FPS", 0.00M, new DateOnly(2003, 09, 12)),
];

app.MapGet("/games", () => games);

app.Run();
