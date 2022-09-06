using FindR.Integrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Text;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Core.Services;
using TutorBuddy.Infrastructure.DataAccess;
using TutorBuddy.Infrastructure.Repository;
using TutorialBuddy.Core;
using TutorialBuddy.Infastructure.Services;
using TutorBuddy.Infrastructure.Seeder;
using TutorBuddy.Core.Utilities;
using AutoMapper;
using TutorBuddy.Core.Enums;
using Microsoft.OpenApi.Models;

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
                .AddTokenProvider<FourDigitTokenProvider>(FourDigitTokenProvider.FourDigitEmail)
                .AddDefaultTokenProviders();

            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = Config.GetValue<string>("RedisCacheUrl");
                opt.InstanceName = "master";
            });

            //builder.Host.UseSerilog((ctx, lc) => lc
            //.WriteTo.Console()
            //.WriteTo.Seq("http://localhost:5341")
            //);

            //Add To DI
            builder.Services.AddScoped<IAuthService,                    AuthService>();
            builder.Services.AddScoped<INotificationService,            NotificationService>();
            builder.Services.AddScoped<IImageUploadService,             ImageUploadService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>),    typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork,                     UnitOfWork>();
            builder.Services.AddScoped<IUserService,                    UserService>();
            builder.Services.AddScoped<IAuthenticationService,          AuthenticationService>();
            builder.Services.AddScoped<IUserRepository,                 UserRepository>();
            builder.Services.AddScoped<ITutorRepository,                TutorRepository>();
            builder.Services.AddScoped<ISubjectRepository,              SubjectRepository>();
            builder.Services.AddScoped<ITokenGeneratorService,          TokenGeneratorService>();
            builder.Services.AddScoped<ITutorSubjectRepository,         TutorSubjectRepository>();
            builder.Services.AddScoped<ISessionRepository,              SessionRepository>();
            builder.Services.AddScoped<ISessionService,                 SessionService>();
            builder.Services.AddScoped<ITutorService, TutorService>();
            builder.Services.AddScoped<IAvailabilityRepository, AvaliabilityRepository>();
            builder.Services.AddScoped<Seeder>();


            // Auto Mapper Registration
            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MapInitializer());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Authentication 
            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Config.GetValue<string>("Google:ClientId");
                googleOptions.ClientSecret = Config.GetValue<string>("Google:ClientSecret");
            })
            .AddJwtBearer(auth =>
            {
                auth.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TokenValidationParameters.DefaultClockSkew,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Config.GetValue<string>("JWT:ValidAudience"),
                    ValidIssuer = Config.GetValue<string>("JWT:ValidIssuer"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.GetValue<string>("JWT:Secret")))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
     
                options.AddPolicy("RequireAdminOnly", policy => policy.RequireRole(UserRole.Admin.ToString()));
                options.AddPolicy("RequireTutorOnly", policy => policy.RequireRole(UserRole.Tutor.ToString()));
                options.AddPolicy("RequireStudentOnly", policy => policy.RequireRole(UserRole.Student.ToString()));
                options.AddPolicy("RequireTutorAndStudent", policy => policy.RequireRole(UserRole.Tutor.ToString(), UserRole.Student.ToString()));
                  
            });



            // Swagger Configuration

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TutorBuddyApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the input below. \r\n\r\n Example :'Bearer 124fsfs' "
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}