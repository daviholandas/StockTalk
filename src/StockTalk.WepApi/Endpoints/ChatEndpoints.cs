using MediatR;
using StockTalk.Application.Commands.ChantRoom;
using StockTalk.Application.Models;
using StockTalk.Application.Services;
using StockTalk.Infra.Data.Queries.ChatRoom;

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

        group.MapGet("",  async (IMediator mediator)
            =>
            {
                var result = await mediator.Send(new GetAllChatsQuery());
                return result.Value;
            })
            .Produces<GetAllChatRoomQueryResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("message", (MessageStock message,
                IMessageBusService service) =>
        {
             service.PublishMessage(message);
        });
            
        return routeBuilder;
    }
}