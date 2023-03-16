using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockTalk.Application.Aggregates.ChatAggregate;
using StockTalk.Application.Aggregates.UserAggregate;

namespace StockTalk.Infra.Data.EntityConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Participants");
        
        builder.HasKey(x => x.Id);

        builder.HasMany<ChatRoom>()
            .WithMany(x => x.Participants);
    }
}