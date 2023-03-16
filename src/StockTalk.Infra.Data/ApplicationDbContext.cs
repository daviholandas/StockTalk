using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StockTalk.Application.Aggregates.ChatAggregate;
using StockTalk.Application.Aggregates.UserAggregate;
using StockTalk.Application.Repositories;

namespace StockTalk.Infra.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<ChatRoom> ChatRooms
        => Set<ChatRoom>();

    public DbSet<User> Participants
        => Set<User>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(120);

        configurationBuilder.Properties<Enum>()
            .HaveConversion<string>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}