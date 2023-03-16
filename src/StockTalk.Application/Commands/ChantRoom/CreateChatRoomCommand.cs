using Ardalis.Result;
using MediatR;

namespace StockTalk.Application.Commands.ChantRoom;

public record struct CreateChatRoomCommand(string Name) :
    IRequest<Result<Guid>>;