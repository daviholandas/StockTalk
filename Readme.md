<p>Install dotnet tools:</p>
dotnet tool restore

<p>Run application migrations:</p>
dotnet-ef database update --project src\StockTalk.Infra.Data\StockTalk.Infra.Data.csproj --startup-project src\StockTalk.WepApi\StockTalk.WepApi.csproj --context StockTalk.Infra.Data.ApplicationDbContext --configuration Debug 20230318234711_Initial

<p> Run auth migrations:</p>
dotnet-ef database update --project src\StockTalk.Infra.Auth\StockTalk.Infra.Auth.csproj --startup-project src\StockTalk.WepApi\StockTalk.WepApi.csproj --context StockTalk.Infra.Auth.Data.AuthDbContext --configuration Debug 20230318234915_Initial