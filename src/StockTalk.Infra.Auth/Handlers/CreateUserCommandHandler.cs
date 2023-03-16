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

    public Task<Result> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
        => throw new NotImplementedException();
}