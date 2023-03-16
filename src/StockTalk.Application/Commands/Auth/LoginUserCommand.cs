using Ardalis.Result;
using MediatR;

namespace StockTalk.Application.Commands.Auth;

public record struct LoginUserCommand(
    string Username,
    string Password) :
    IRequest<Result<string>>;