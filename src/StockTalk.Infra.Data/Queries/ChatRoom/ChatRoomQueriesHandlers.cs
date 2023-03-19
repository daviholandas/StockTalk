using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockTalk.Application.Repositories;

namespace StockTalk.Infra.Data.Queries.ChatRoom;

public class ChatRoomQueriesHandlers :
    IRequestHandler<GetAllChatsQuery, Result<IEnumerable<GetAllChatRoomQueryResult>>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public ChatRoomQueriesHandlers(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<IEnumerable<GetAllChatRoomQueryResult>>> Handle(GetAllChatsQuery query,
        CancellationToken cancellationToken)
    {
        var chats = await _applicationDbContext
            .ChatRooms
            .AsNoTracking()
            .Select(x => new GetAllChatRoomQueryResult(x.Id, x.Name, x.Status.ToString()))
            .ToListAsync(cancellationToken);

        return Result<IEnumerable<GetAllChatRoomQueryResult>>
            .Success(chats ?? Enumerable.Empty<GetAllChatRoomQueryResult>());
    }
}