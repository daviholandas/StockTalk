using Microsoft.OpenApi.Models;
using StockTalk.Application.IoC;
using StockTalk.Infra.Auth.IoC;
using StockTalk.Infra.Data.IoC;
using StockTalk.Infra.MessageBus.IoC;
using StockTalk.WepApi.Endpoints;
using StockTalk.WepApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new ()
    {
        Description = """
            JWT Authorization header using the Bearer scheme. 
            Enter 'Bearer' [space] and then your token in the text input below.
        """,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new ()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddSignalR(x => x.EnableDetailedErrors = true);

builder.Services
    .AddApplicationIoC()
    .AddInfraDataIoC(builder.Configuration)
    .AddInfraAuthIoC(builder.Configuration);

builder.Services
    .AddRabbitMqBroker(builder.Configuration);

builder.Services
    .AddCors(coreOptions => 
    coreOptions
        .AddDefaultPolicy(policy =>
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowCredentials()));

builder.Services
    .AddMemoryCache();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();


app
    .AddChatEndpoints()
    .AddAuthEndpoints();

app.MapHub<ChatHub>("/chats")
    .RequireAuthorization();

app.MapHub<BotHub>("/bot")
    .RequireAuthorization();

app.Run();