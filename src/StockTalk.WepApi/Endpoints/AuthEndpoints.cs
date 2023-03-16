using MediatR;
using StockTalk.Application.Commands.Auth;

namespace StockTalk.WepApi.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder AddAuthEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder
            .MapGroup("auth")
            .WithTags("Auth")
            .WithOpenApi();

        group.MapPost("register",
            async (CreateUserCommand command, IMediator mediator)
                => await mediator.Send(command)
                    switch
                    {
                        { IsSuccess: true } result => Results.Created("", result.Value),
                        var result => Results.BadRequest(result.Errors)
                    });
        
        return endpointRouteBuilder;
    }
}