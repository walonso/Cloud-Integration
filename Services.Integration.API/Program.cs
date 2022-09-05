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
//var multiplexer = ConnectionMultiplexer.Connect("localhost");  //local
string connectionStringRedis="walonsotestredis.redis.cache.windows.net,abortConnect=false,ssl=true,allowAdmin=true,password=8yBDzKhsfz4O5HwLaUU7Y0cW3Sv1PalFYAzCaEx6JR8=";
var multiplexer = ConnectionMultiplexer.Connect(connectionStringRedis);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
//Service bus
string connectionStringBus = "Endpoint=sb://walservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ETUI7sFtic30jyTOQW+t8SToGqStjjqHGkFQ+vGEBUQ=";
//services.AddSingleton<IMyInterface, MyInterface>();
//builder.Services.AddSingleton<IMessageQueueService>(provider => new ServiceBusQueueIntegration(connectionStringBus)); //, provider.GetService<IMyInterface>()));
builder.Services.AddSingleton<IMessageQueueService>(x =>
      ActivatorUtilities.CreateInstance<ServiceBusQueueIntegration>(x, connectionStringBus));


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
