using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Adds validation
builder.Services.AddValidation();

// Add DbContext via DI 
var connString = "DataSource=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
