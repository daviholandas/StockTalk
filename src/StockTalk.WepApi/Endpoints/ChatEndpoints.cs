using MediatR;
using StockTalk.Application.Commands.ChantRoom;
using StockTalk.Infra.Data.Queries.ChatRoom;

namespace StockTalk.WepApi.Endpoints;

public static class ChatEndpoints
{
    public static IEndpointRouteBuilder AddChatEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder
            .MapGroup("chat")
            .WithTags("Chat")
            .WithOpenApi();

        group.MapPost("", async (CreateChatRoomCommand commnand,
                IMediator mediator)
            => await mediator.Send(commnand)
                switch
                {
                    { IsSuccess: true } result => Results.Created($"{result.Value}",result.Value),
                    _ => Results.BadRequest()
                });

        group.MapGet("", async (IMediator mediator)
            => await mediator.Send(new GetAllChatsQuery())
                switch
            {
                var result => Results.Ok(result.Value),
            });
            
        return routeBuilder;
    }
}