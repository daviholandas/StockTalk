using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using StockTalk.Application.Commands.Auth;

namespace StockTalk.Infra.Auth.Handlers;

public class LoginUserCommandHandler :
    IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public LoginUserCommandHandler(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Result<string>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(
            request.Username,
            request.Password,
            false,
            true);

        if (!signInResult.Succeeded)
            return Result<string>
                .Error("Something is wrong with you credentials.");
        
        if (signInResult.IsLockedOut)
            return Result<string>
                .Error("You are locked out!");
        
        return Result<string>
            .Success(await GenerateJwtTokens(request.Username));
    }

    private async ValueTask<string> GenerateJwtTokens(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        List<Claim> claims = new()
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
        };
        
        return new JwtSecurityTokenHandler()
            .WriteToken(new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: _jwtOptions.SigningCredentials,
                expires: DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration)));
    }
}