using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Services;
using Services.Integration.Infrastructure.Azure;
using Services.Integration.Infrastructure.Azure.Authentication;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
var env=Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

// Add services to the container.
builder.Services.AddSingleton<ISecretStorage, KeyVaultIntegration>();
builder.Services.AddSingleton<ISecretStorageService, SecretStorageService>();
builder.Services.AddSingleton<ICacheDatabase, RedisIntegration>();
builder.Services.AddSingleton<ICacheDatabaseService, CacheDatabaseService>();
//var multiplexer = ConnectionMultiplexer.Connect("localhost");  //local
string connectionStringRedis="walonsotestredis.redis.cache.windows.net,abortConnect=false,ssl=true,allowAdmin=true,password=[PASSWORD]";
var multiplexer = ConnectionMultiplexer.Connect(connectionStringRedis);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
//Service bus
string connectionStringBus = "Endpoint=sb://bootcamparroyoservicebus.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=[KEY]";
//services.AddSingleton<IMyInterface, MyInterface>();
//builder.Services.AddSingleton<IMessageQueueService>(provider => new ServiceBusQueueIntegration(connectionStringBus)); //, provider.GetService<IMyInterface>()));
builder.Services.AddSingleton<IMessageQueueService>(x =>
      ActivatorUtilities.CreateInstance<ServiceBusQueueIntegration>(x, connectionStringBus));

if (env == "Development")
{
    builder.Services.AddSingleton<IDefaultAzureCredentialAuthentication, LocalAuthentication>();
    //builder.Services.AddSingleton<BlobServiceClient>(x =>
    //    new BlobServiceClient(
    //        new Uri("https://<account-name>.blob.core.windows.net"),
    //        new DefaultAzureCredential()));
    //builder.Services.AddSingleton<IBlobStorageService, StorageAccountIntegration>(sp =>
    //{
    //    var dbContext = sp.GetRequiredService<IDefaultAzureCredentialAuthentication>().GetAuthentication();
    //    return new StorageAccountIntegration();
    //});
} else
{
    var managedIdentity = builder.Configuration.GetSection("USER_MANAGED_IDENTITY_ID").Value;
    builder.Services.AddSingleton<IDefaultAzureCredentialAuthentication>(new ManagedIdentityAuthentication(managedIdentity));
    //builder.Services.AddSingleton<BlobServiceClient>(x =>
    //    new BlobServiceClient(
    //        new Uri("https://<account-name>.blob.core.windows.net"),
    //        new DefaultAzureCredential()));
}
builder.Services.AddSingleton<IBlobStorageService, StorageAccountIntegration>();





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
