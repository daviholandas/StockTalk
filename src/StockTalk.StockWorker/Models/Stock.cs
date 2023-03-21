using CsvHelper.Configuration.Attributes;

namespace StockTalk.StockWorker.Models;

public record Stock([Name("Symbol")]string Symbol,
[Name("Close")]string Value);