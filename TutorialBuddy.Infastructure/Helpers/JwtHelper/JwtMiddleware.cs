
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FindR.Infastructure.Helpers.JwtHelper
{
    public class JwtMiddleware
    {
        public RequestDelegate Next { get; }
        public JWTAppSettings AppSettings { get; }

        public JwtMiddleware(RequestDelegate next, IOptions<JWTAppSettings> appSettings)
        {
            Next = next;
            AppSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?
                .Split(" ")
                .Last();
            if (token != null)
            {
                await attachUserToContext(context, userService, token);
            }
            await Next(context);
        }

        private async Task attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSettings.Secret!);
                var result = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var UserId = (jwtToken.Claims.First(x => x.Type == "id").Value);
                context.Items["User"] = await userService.GetByIdAsync(UserId);
            }
            catch (Exception ex) 
            {
            }
        }
    }
}