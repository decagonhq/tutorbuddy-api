using Amazon.Extensions.NETCore.Setup;
using FindRApi.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TutorBuddy.Infrastructure.DataAccess;
using TutorBuddy.Infrastructure.Seeder;
using TutorBuddyApi;
using TutorBuddyApi.Middleware;
using TutorialBuddy.Infastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var services = builder.Services;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.RegisterServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Registering Serilog as a log provider
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<Seeder>();
db.Seed().GetAwaiter().GetResult();


//Options Bindings
var cloudinaryOptions = new CloudinarySettings();
configuration.GetSection("CloudinarySettings").Bind(cloudinaryOptions);

//aws secrets

builder.Configuration.AddSystemsManager("/development/", new AWSOptions
{
    Region = Amazon.RegionEndpoint.USWest2
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
 
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();


app.MapControllers();

logger.Information("Lets the ball role...");

app.Run();