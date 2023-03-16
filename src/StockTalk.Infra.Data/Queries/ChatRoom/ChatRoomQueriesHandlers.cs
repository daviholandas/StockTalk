using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockTalk.Application.Repositories;

namespace StockTalk.Infra.Data.Queries.ChatRoom;

public class ChatRoomQueriesHandlers :
    IRequestHandler<GetAllChatsQuery, Result<IEnumerable<Application.Aggregates.ChatAggregate.ChatRoom>>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public ChatRoomQueriesHandlers(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<IEnumerable<Application.Aggregates.ChatAggregate.ChatRoom>>> Handle(GetAllChatsQuery query,
        CancellationToken cancellationToken)
        => Result<IEnumerable<Application.Aggregates.ChatAggregate.ChatRoom>>
            .Success(await _applicationDbContext.ChatRooms.ToListAsync(cancellationToken));
}