using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Forms.Application.UserDir;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Forms.Infrastructure.Providers;

public class JwtHandler
{
    private readonly string _jwtValidIssuer;
    private readonly string _jwtValidAudience;
    private readonly string _jwtSecurityKey;
    private readonly double _jwtExpiryInMinutes;
    private readonly IUsersRepository _usersRepository;

    public JwtHandler(
        IUsersRepository usersRepository)
    {
         Env.Load();

         _jwtValidIssuer = Env.GetString("JWT_ISSUER");
         _jwtValidAudience = Env.GetString("JWT_AUDIENCE");
         _jwtSecurityKey = Env.GetString("JWT_SECRET");

         if (!double.TryParse(
                 Env.GetString("JWT_EXPIRY_IN_MINUTES"), 
                 out _jwtExpiryInMinutes))
         {
             throw new InvalidOperationException("JWT expiry time is not configured correctly.");
         }

         _usersRepository = usersRepository;
    }

    public string CreateToken(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims.Result);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSecurityKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.FullName.FirstName),
            new Claim(ClaimTypes.Surname, user.FullName.LastName),
        };

        Guid.TryParse(user.Id, out var userId);
        
        var roleResult = await _usersRepository.GetUserRole(userId);

        if (roleResult.IsSuccess)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleResult.Value));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtValidIssuer,
            audience: _jwtValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtExpiryInMinutes),
            signingCredentials: credentials
        );

        return tokenOptions;
    }
}