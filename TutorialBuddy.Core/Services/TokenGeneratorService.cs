using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public TokenGeneratorService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> GenerateToken(User user)
        {
            var avatar = user.AvatarUrl ??= "https://cdn3.iconfinder.com/data/icons/sharp-users-vol-1/32/-_Default_Account_Avatar-512.png";

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Uri, avatar)
            };

            //Gets the roles of the logged in user and adds it to Claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Secret")));

            // Specifying JWTSecurityToken Parameters
            var token = new JwtSecurityToken
            (audience: _configuration.GetValue<string>("JWT:ValidAudience"),
             issuer: _configuration.GetValue<string>("JWT:ValidIssuer"),
             claims: authClaims,
             expires: user.RefreshTokenExpiryTime,
             signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Guid GenerateRefreshToken()
        {
            return Guid.NewGuid();
        }
    }
}
