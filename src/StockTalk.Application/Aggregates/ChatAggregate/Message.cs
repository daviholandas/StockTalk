namespace StockTalk.Application.Aggregates.ChatAggregate;

public record Message(DateTime SentAt,
    string SentTo,
    string Body);