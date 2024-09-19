using WebTech.Application;
using WebTech.Persistence;
using WebTech.WebApi;
using WebTech.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddCustomConfigurations();

builder.Services.AddApplication()
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseHostFilteringMiddleware();

app.MapControllers();

app.MapGet("/", () => $"Welcome to the Home Page WebTech API!\nUTC Time: {DateTime.UtcNow}");


app.Run();