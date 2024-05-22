var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

builder.Services.AddCarter();

app.Run();
