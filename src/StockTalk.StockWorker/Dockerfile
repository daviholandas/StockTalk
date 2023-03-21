FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/StockTalk.StockWorker/StockTalk.StockWorker.csproj", "src/StockTalk.StockWorker/"]
RUN dotnet restore "src/StockTalk.StockWorker/StockTalk.StockWorker.csproj"
COPY . .
WORKDIR "/src/src/StockTalk.StockWorker"
RUN dotnet build "StockTalk.StockWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockTalk.StockWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockTalk.StockWorker.dll"]
