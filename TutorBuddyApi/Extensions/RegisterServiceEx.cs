using FindR.Integrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Text;
using TutorialBuddy.Core.Models;
using TutorialBuddy.DataAccess;

namespace FindRApi.Extensions
{
    public static class RegisterServiceEx
    {
        /// <summary>
        /// Registers services to the DI container
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var Config = builder.Configuration;
            //builder.Services.AddSingleton<ISignalRHubHelper, SignalRHubHelper>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            var connStr = DatabaseSetup.DatabaseConnectionString(builder.Environment, Config);
            var dbBuilder = new NpgsqlConnectionStringBuilder(connStr)
            {
                // Todo: add to secrets
                // Password = "postgres"
            };

            builder.Services.AddDbContext<TutorialBuddyContext>(opt => opt.UseNpgsql(connStr)

            );
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TutorialBuddyContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = Config["RedisCacheUrl"];
                opt.InstanceName = "master";
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(auth =>
            {
                auth.SaveToken = true;
                auth.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TokenValidationParameters.DefaultClockSkew,
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    ValidAudience = Config["JWT:ValidAudience"],
                    ValidIssuer = Config["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["AppSettings:Secret"]))
                };
            });
        }
    }
}