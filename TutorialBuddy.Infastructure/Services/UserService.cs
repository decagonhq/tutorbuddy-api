﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TutorialBuddy.Core.Models;

public interface IUserService
{
    Task<User> GetByIdAsync(string userId);

    Task<ResponseModel> Authenticate(RequestModel requestModel);
}

public class UserService : IUserService
{
    private readonly IOptions<JWTAppSettings> jwtSetting;
    private readonly UserManager<User> userManager;

    public UserService(IOptions<JWTAppSettings> jwtSetting, UserManager<User> userManager)
    {
        this.jwtSetting = jwtSetting;
        this.userManager = userManager;
    }

    public async Task<ResponseModel> Authenticate(RequestModel requestModel)
    {
        var user = await userManager.FindByEmailAsync(requestModel.Email);
        if (user == null) return null;
        var token = await GenerateJwtToken(user);
        return new ResponseModel(user, token);
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var roles = new List<Claim>();
        var userRoles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSetting.Value.Secret!);

        foreach (var role in userRoles)
        {
            roles.Add(new Claim(ClaimTypes.Role, role));
        }
        roles.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        roles.Add(new Claim(ClaimTypes.Email, user.Email));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(roles),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            IssuedAt = DateTime.UtcNow,
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<User> GetByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId);
    }
}