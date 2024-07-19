using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gamestore.BLL.Identity.JWT;

public static class JwtHelpers
{
    public static string UserIdClaim { get; } = "UserId";

    public static async Task<string> GenerateJwtToken(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration, AppUser? user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.UserName!),
            new(UserIdClaim, user.Id),
        };

        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
            var appRole = await roleManager.FindByNameAsync(role);
            var roleClaims = await roleManager.GetClaimsAsync(appRole!);
            claims.AddRange(roleClaims);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        issuer: configuration["Jwt:Issuer"],
        audience: configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(10),
        signingCredentials: creds);

        var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return generatedToken;
    }
}
