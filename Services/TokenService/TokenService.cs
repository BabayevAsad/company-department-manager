using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Company_Expense_Tracker.Entities;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Company_Expense_Tracker.Services.TokenService;

public class TokenService
{
    public readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateToken(User? user)
    {
        var token = new Token();

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration["Token:SecurityKey"]));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(_configuration["Token:Expiration"]));
        
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        JwtSecurityToken jwtSecurityToken = new(
            issuer : _configuration["Token:issuer"],
            audience : _configuration["Token:audience"],
            expires : token.Expiration,
            notBefore : DateTime.Now,
            claims : claims,
            signingCredentials : credentials
        );
        
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

        var numbers = new byte[32];

        using var random = RandomNumberGenerator.Create();

        random.GetBytes(numbers);
        token.RefreshToken = Convert.ToBase64String(numbers);
        return token;
    }
}