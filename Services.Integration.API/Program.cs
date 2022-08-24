using Services.Integration.Infrastructure.Azure;
using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISecretStorage, KeyVaultIntegration>();
builder.Services.AddSingleton<ISecretStorageService, SecretStorageService>();
builder.Services.AddSingleton<ICacheDatabase, RedisIntegration>();
builder.Services.AddSingleton<ICacheDatabaseService, CacheDatabaseService>();
var multiplexer = ConnectionMultiplexer.Connect("localhost");
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
    

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
