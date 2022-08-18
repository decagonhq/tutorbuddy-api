using Amazon.CloudWatchLogs;
using Amazon.Extensions.NETCore.Setup;
using FindRApi.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;
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

//aws secrets
builder.Configuration.AddSystemsManager("/development/", new AWSOptions
{
    Region = Amazon.RegionEndpoint.USEast2
});


builder.RegisterServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Registering Serilog as a log provider
var client = new AmazonCloudWatchLogsClient();
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .MinimumLevel.Verbose()
  .WriteTo.AmazonCloudWatch(
        // The name of the log group to log to
        logGroup: "/ecs/tutorbuddy-api-td",
        // A string that our log stream names should be prefixed with. We are just specifying the
        // start timestamp as the log stream prefix
        logStreamPrefix: DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
        // The AWS CloudWatch client to use
        cloudWatchClient: client)
  .WriteTo.Console()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<Seeder>();
db.Seed().GetAwaiter().GetResult();




// Configure the HTTP request pipeline.

 
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tutor Buddy Api v1");
    
});

app.UseStaticFiles();
app.UseRouting();
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

logger.Verbose("Lets the ball role...");
Log.Logger = logger;

app.Run();