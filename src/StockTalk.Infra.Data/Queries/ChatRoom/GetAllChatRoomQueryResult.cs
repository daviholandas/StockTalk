namespace StockTalk.Infra.Data.Queries.ChatRoom;

public record struct GetAllChatRoomQueryResult(
    Guid Id,
    string Name, 
    string Status);