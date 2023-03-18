using Microsoft.EntityFrameworkCore;
using StockTalk.Application.Aggregates.ChatAggregate;

namespace StockTalk.Application.Repositories;

public interface IApplicationDbContext
{
    DbSet<ChatRoom> ChatRooms { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}