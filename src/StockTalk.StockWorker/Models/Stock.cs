using CsvHelper.Configuration.Attributes;

namespace StockTalk.StockWorker.Models;

public class Stock
{
    [Name("Symbol")]
    public string Symbol { get; set; }
    
    [Name("Close")]
    public string Value { get; set; }
}