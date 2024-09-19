using WebTech.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHostFilteringMiddleware();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();