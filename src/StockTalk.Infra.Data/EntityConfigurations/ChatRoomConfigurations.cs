using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockTalk.Application.Aggregates.ChatAggregate;

namespace StockTalk.Infra.Data.EntityConfigurations;

public class ChatRoomConfigurations : IEntityTypeConfiguration<ChatRoom>
{

    public void Configure(EntityTypeBuilder<ChatRoom> builder)
    {
        builder.ToTable("Chats", 
            t =>
            t.IsTemporal());

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.ChatHistory,
            navigationBuilder => navigationBuilder.ToJson());
    }
}