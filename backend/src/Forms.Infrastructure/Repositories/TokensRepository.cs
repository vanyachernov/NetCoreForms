using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;
using DotNetEnv;
using Forms.Application.DTOs;
using Forms.Application.UserDir;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.ValueObjects;
using Forms.Infrastructure.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Forms.Infrastructure.Repositories;

public class TokensRepository : ITokensRepository
{
    private readonly string _jwtSecurityKey;
    private readonly string _jwtValidIssuer;
    private readonly string _jwtValidAudience;
    private readonly double _jwtExpiryInMinutes;
    private readonly JwtHandler _jwtHandler;
    private readonly UserManager<User> _userManager;
    private readonly IUsersRepository _usersRepository;
    
    private User? _user;

    public TokensRepository(
        UserManager<User> userManager,
        JwtHandler jwtHandler,
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

        _userManager = userManager;
        _jwtHandler = jwtHandler;
        _usersRepository = usersRepository;
    }

    public async Task<Result<TokenDto, Error>> CreateToken(
        User user, 
        bool populateExp, 
        CancellationToken cancellationToken = default)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(
            signingCredentials, 
            claims);

        var refreshToken = Domain.TemplateManagement.ValueObjects
            .RefreshToken.Create(GenerateRefreshToken());

        if (refreshToken.IsFailure)
        {
            return Errors.General.ValueIsInvalid("RefreshToken");
        }

        user.SetRefreshToken(refreshToken.Value);

        var refreshTokenExpiryTime = RefreshTokenExpiryTime
            .Create(DateTime.UtcNow.AddDays(7));

        if (refreshTokenExpiryTime.IsFailure)
        {
            return Errors.General.ValueIsInvalid("RefreshTokenExpiryTime");
        }

        if (populateExp)
        {
            user.SetRefreshTokenExpiryTime(refreshTokenExpiryTime.Value);
        }

        await _userManager.UpdateAsync(user);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new TokenDto(accessToken, refreshToken.Value.Value);
    }

    public async Task<Result<TokenDto, Error>> RefreshToken(
        User user,
        TokenDto tokenDto, 
        CancellationToken cancellationToken = default)
    {
        var principal = _jwtHandler.GetPrincipalFromExpiredTone(tokenDto.AccessToken);

        if (user.RefreshToken.Value != tokenDto.RefreshToken || 
            user.RefreshTokenExpiryTime.Value <= DateTime.UtcNow)
        {
            throw new SecurityTokenException("Invalid refresh token or token expired.");
        }

        _user = user;

        var createTokenResult = await CreateToken(
            user, 
            populateExp: false, 
            cancellationToken);

        if (createTokenResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("CreatedToken");
        }

        return createTokenResult;
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
            new Claim("userId", user.Id),
            new Claim("userEmail", user.Email!),
            new Claim("userFirstname", user.FullName.FirstName),
            new Claim("userLastname", user.FullName.LastName),
        };

        Guid.TryParse(user.Id, out var userId);
        
        var roleResult = await _usersRepository.GetUserRole(userId);

        if (roleResult.IsSuccess)
        {
            claims.Add(new Claim("userRole", roleResult.Value));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(
        SigningCredentials credentials, 
        List<Claim> claims)
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

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        
        using var random = RandomNumberGenerator.Create();
        
        random.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredTone(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtValidIssuer,
            ValidAudience = _jwtValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecurityKey!))
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid Token");
        }

        return principal;
    }
}