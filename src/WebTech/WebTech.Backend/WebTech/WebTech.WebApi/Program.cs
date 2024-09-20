using WebTech.Application;
using WebTech.Application.Common.Converters;
using WebTech.Domain.Enums;
using WebTech.Persistence;
using WebTech.WebApi;
using WebTech.WebApi.Middleware;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new SnakeCaseStringEnumConverter<ErrorCode>());
});

builder.AddCustomConfigurations();

builder.Services.AddApplication()
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseHostFilteringMiddleware();

app.MapControllers();

app.MapGet("/", () => $"Welcome to the Home Page WebTech API!\nUTC Time: {DateTime.UtcNow}");


app.Run();