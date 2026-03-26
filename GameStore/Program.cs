using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Adds validation
builder.Services.AddValidation();

builder.AddGameStoreDb();

var app = builder.Build();

app.MapGamesEndpoints();

app.MapGenresEndpoints();

app.MigrateDb();

app.Run();