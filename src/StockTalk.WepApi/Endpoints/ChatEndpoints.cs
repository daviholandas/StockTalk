using MediatR;
using StockTalk.Application.Commands.ChantRoom;

namespace StockTalk.WepApi.Endpoints;

public static class ChatEndpoints
{
    public static IEndpointRouteBuilder AddChatEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder
            .MapGroup("chat")
            .WithTags("Chat")
            .RequireAuthorization()
            .WithOpenApi();

        group.MapPost("", async (CreateChatRoomCommand commnand,
                IMediator mediator)
            => await mediator.Send(commnand)
                switch
                {
                    { IsSuccess: true } result => Results.Ok(),
                    _ => Results.BadRequest()
                });

        group.MapGet("",  ()
            => Results.Ok(new {ChatRoom = "Stocks"}));
            
        return routeBuilder;
    }
}