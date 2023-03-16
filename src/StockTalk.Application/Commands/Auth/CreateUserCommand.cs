using Ardalis.Result;
using MediatR;

namespace StockTalk.Application.Commands.Auth;

public record struct CreateUserCommand(string Name,
    string Email, 
    string NickName,
    string Password) :
    IRequest<Result>;