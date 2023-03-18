namespace StockTalk.Application.Aggregates.ChatAggregate;

public record Message(string SentAt,
    string SentTo,
    string Body);