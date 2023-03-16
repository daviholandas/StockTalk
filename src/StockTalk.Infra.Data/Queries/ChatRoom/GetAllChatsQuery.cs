using Ardalis.Result;
using MediatR;

namespace StockTalk.Infra.Data.Queries.ChatRoom;

public record struct GetAllChatsQuery()
    : IRequest<Result<IEnumerable<Application.Aggregates.ChatAggregate.ChatRoom>>>;