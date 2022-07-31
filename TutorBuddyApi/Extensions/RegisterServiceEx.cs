using FindR.Integrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Serilog;
using System.Text;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Services;
using TutorBuddy.Infrastructure.Repository;
using TutorialBuddy.Core;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;
using TutorialBuddy.Infastructure.Services;

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

            var connStr = DatabaseSetup.DatabaseConnectionString(builder.Environment, Config);
            var dbBuilder = new NpgsqlConnectionStringBuilder(connStr);

            builder.Services.AddDbContext<TutorBuddyContext>(opt => opt.UseNpgsql(connStr));

            builder.Services.AddIdentity<User, IdentityRole>(x =>
                {
                    x.Password.RequiredLength = 8;
                    x.Password.RequireDigit = false;
                    x.Password.RequireUppercase = true;
                    x.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<TutorBuddyContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = Config["RedisCacheUrl"];
                opt.InstanceName = "master";
            });

            //builder.Host.UseSerilog((ctx, lc) => lc
            //.WriteTo.Console()
            //.WriteTo.Seq("http://localhost:5341")
            //);

            //Add To DI
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IImageUploadService, ImageUploadService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserService, UserService>();

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