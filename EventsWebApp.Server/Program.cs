using EventsWebApp.Server;
using EventsWebApp.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment, args);
app.MigrateDatabase();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.MapGet("get", () =>
{
    return Results.Ok("ok");
}).RequireAuthorization("AdminPolicy");


app.Run();
