FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/StockTalk.WepApi/StockTalk.WepApi.csproj", "src/StockTalk.WepApi/"]
RUN dotnet restore "src/StockTalk.WepApi/StockTalk.WepApi.csproj"
COPY . .
WORKDIR "/src/src/StockTalk.WepApi"
RUN dotnet build "StockTalk.WepApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockTalk.WepApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockTalk.WepApi.dll"]
