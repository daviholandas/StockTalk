namespace StockTalk.StockWorker.Models;

public class WorkerSettings
{
    public ClientSettings ClientSettings { get; set; }
}

public class ClientSettings
{
    public string Name { get; set; } = string.Empty;

    public string BaseUrl { get; set; } = string.Empty;
}