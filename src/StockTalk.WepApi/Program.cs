using StockTalk.Application.IoC;
using StockTalk.Infra.Data.IoC;
using StockTalk.WepApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplicationIoC()
    .AddInfraDataIoC(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddChatEndpoints();

app.Run();