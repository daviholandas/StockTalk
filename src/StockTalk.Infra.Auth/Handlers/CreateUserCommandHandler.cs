using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StockTalk.Application.Commands.Auth;

namespace StockTalk.Infra.Auth.Handlers;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Result>
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateUserCommandHandler(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Result> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        IdentityUser newUser = new()
        {
            UserName = request.Name,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(newUser);

        if (result.Succeeded)
            await _userManager.SetLockoutEnabledAsync(newUser, false);

        return result.Succeeded ?
            Result.Success() : 
            Result.Error(result.Errors.Select(x => x.Description).ToArray());
    }
}