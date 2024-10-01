using EventsWebApp.Server;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.MapGet("get", () =>
{
    return Results.Ok("ok");
}).RequireAuthorization("AdminPolicy");


app.Run();
