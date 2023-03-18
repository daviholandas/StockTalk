using Ardalis.Result;
using MediatR;
using StockTalk.Application.Aggregates.ChatAggregate;
using StockTalk.Application.Repositories;

namespace StockTalk.Application.Commands.ChantRoom;

public class ChatRoomCommandsHandlers :
    IRequestHandler<CreateChatRoomCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public ChatRoomCommandsHandlers(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<Guid>> Handle(CreateChatRoomCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var entry = await _applicationDbContext.ChatRooms
                .AddAsync(new ChatRoom(command.Name));
        
            var commitResult = await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(entry.Entity.Id);
        }
        catch (Exception e)
        {
            return Result<Guid>.Error(e.Message);
        }
    }
}