using System.Globalization;
using Ardalis.Result;
using CsvHelper;
using Microsoft.Extensions.Options;
using StockTalk.StockWorker.Models;

namespace StockTalk.StockWorker.Services;

public interface IStockService
{
    ValueTask<Result<Stock>> GetStockBySymbol(string symbol);
}

public class StockService : IStockService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationSettings _applicationSettings;

    public StockService(IHttpClientFactory httpClientFactory,
        IOptions<ApplicationSettings> applicationSettings)
    {
        _applicationSettings = applicationSettings.Value;
        _httpClient = httpClientFactory
            .CreateClient(_applicationSettings.ClientSettings.Name);
    }

    public async ValueTask<Result<Stock>> GetStockBySymbol(string symbol)
    {
        try
        {
            var stream = await _httpClient
                .GetStreamAsync($"?s={symbol.ToLower()}&f=sd2t2ohlcv&h&e=csv")
                .ConfigureAwait(false);
        
            using var reader = new StreamReader(stream);
        
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return Result<Stock>.Success(csv.GetRecord<Stock>());
        }
        catch (Exception e)
        {
            return Result<Stock>.Error(e.Message);
        }
    }
} 