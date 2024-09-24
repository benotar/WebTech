using WebTech.Application;
using WebTech.Application.Common.Converters;
using WebTech.Application.Configurations;
using WebTech.Domain.Enums;
using WebTech.Persistence;
using WebTech.WebApi;
using WebTech.WebApi.Middleware;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
 
var corsConfig = new CorsConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind(CorsConfiguration.ConfigurationKey, corsConfig);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new SnakeCaseStringEnumConverter<ErrorCode>());
});

builder.AddCustomConfigurations();

builder.Services.AddApplication()
    .AddAuth(builder.Configuration)
    .AddPersistence(builder.Configuration)
    .AddRedis(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsConfig.PolicyName, policy =>
    {
        policy
            .WithOrigins(corsConfig.AllowedOrigins.ToArray())
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseHostFilteringMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(corsConfig.PolicyName);

app.MapControllers();

app.MapGet("/", () => $"Welcome to the Home Page WebTech API!\nUTC Time: {DateTime.UtcNow}");


app.Run();