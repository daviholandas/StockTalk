using StockTalk.Application.Aggregates.UserAggregate;
using StockTalk.Application.Common;

namespace StockTalk.Application.Aggregates.ChatAggregate;

public class ChatRoom : Entity
{
    public ChatRoom(string name, 
        List<User> participants)
    {
        Name = name;
        Participants = participants;
        Status = Status.Open;
    }
    
    private ChatRoom(){}

    public string Name { get; private set; }

    public List<Message>? ChatHistory { get; private set; } = new();

    public List<User> Participants { get; private set; }

    public Status Status { get; private set; }
}

public enum Status
{
    Open,
    Closed
}
