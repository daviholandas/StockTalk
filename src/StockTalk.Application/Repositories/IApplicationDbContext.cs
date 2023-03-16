using Microsoft.EntityFrameworkCore;
using StockTalk.Application.Aggregates.ChatAggregate;
using StockTalk.Application.Aggregates.UserAggregate;

namespace StockTalk.Application.Repositories;

public interface IApplicationDbContext
{
    DbSet<ChatRoom> ChatRooms { get; }
    
    DbSet<User> Participants { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}