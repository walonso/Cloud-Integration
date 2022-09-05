Goal: Azure integration in .net 6.0.  
  
1. Prerequisites:  
Visual studio code 2022 community. Or  https://visualstudio.microsoft.com/es/vs/community/  
Visual studio code.  https://code.visualstudio.com/  
Download .nt 6.0: https://dotnet.microsoft.com/en-us/download/dotnet/6.0  
  
2. add .net 6.0 to visual studio code:  
https://www.c-sharpcorner.com/article/how-to-setup-visual-studio-code-for-c-sharp-10-and-net-6-0/  
  
3. Create solution:  
dotnet new sln --name Services.Integration  
  
4. create projects:  
https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new  
dotnet new webapi -f net6.0 -n Services.Integration.API  
dotnet new classlib -n Services.Integration.Application  
dotnet new classlib -n Services.Integration.Infrastructure  
  
  
5. Add projects to solution:  
dotnet sln Services.Integration.sln add Services.Integration.API/Services.Integration.API.csproj  
dotnet sln Services.Integration.sln add Services.Integration.Application/Services.Integration.Application.csproj  
dotnet sln Services.Integration.sln add Services.Integration.Infrastructure/Services.Integration.Infrastructure.csproj  
  
6. Add references within projects.  
dotnet add Services.Integration.API/Services.Integration.API.csproj reference Services.Integration.Application/Services.Integration.Application.csproj  
dotnet add Services.Integration.Infrastructure/Services.Integration.Infrastructure.csproj reference Services.Integration.Application/Services.Integration.Application.csproj  
dotnet add Services.Integration.API/Services.Integration.API.csproj reference Services.Integration.Infrastructure/Services.Integration.Infrastructure.csproj  
  
7. run project:  
dotnet run --project Services.Integration.API/Services.Integration.API.csproj  
  
8. Add references:  
8.1. KeyVault:  
cd Services.Integration.Infrastructure  
dotnet add package Azure.Security.KeyVault.Secrets  
dotnet add package Azure.Identity  
  
- run the project:  
az login  
https://localhost:7068/Secret?secretName=newsecret  

8.2. Redis:
https://docs.microsoft.com/en-us/azure/azure-cache-for-redis/cache-dotnet-core-quickstart

8.2.1 To work locally:
https://redis.io/docs/getting-started/installation/install-redis-on-windows/
https://developer.redis.com/create/windows/

8.2.2 C#:
https://docs.microsoft.com/en-us/azure/azure-cache-for-redis/cache-dotnet-core-quickstart
https://docs.redis.com/latest/rs/references/client_references/client_csharp/

-> cd Services.Integration.Infrastructure
-> dotnet add package StackExchange.Redis
-> cd Services.Integration.API
-> dotnet add package StackExchange.Redis

-> url:
https://localhost:7068/CacheDatabase?key=user:1  (get)
https://localhost:7068/CacheDatabase/w/li   (set)

Use UI tool:
https://azure.microsoft.com/en-us/blog/view-your-azure-cache-for-redis-data-in-new-visual-studio-code-extension/

8.3 Service Bus
https://www.code4it.dev/blog/azure-service-bus-introduction

-> cd Services.Integration.Infrastructure
-> dotnet add package Azure.Messaging.ServiceBus

Create ServiceBus and Queue:
https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues

-> browse message in ServiceBus
https://docs.microsoft.com/es-es/azure/service-bus-messaging/explorer

-> url:
https://localhost:7068/MessageQueue/SetMessage/messageInQueue  (set)
https://localhost:7068/MessageQueue  (get)

-> .Net:
https://docs.microsoft.com/en-us/dotnet/api/overview/azure/messaging.servicebus-readme
https://docs.microsoft.com/en-us/dotnet/api/overview/azure/messaging.servicebus-readme#aspnet-core
ttl -> https://docs.microsoft.com/es-es/azure/service-bus-messaging/message-expiration?WT.mc_id=Portal-Microsoft_Azure_ServiceBus
duplication -> https://docs.microsoft.com/es-es/azure/service-bus-messaging/duplicate-detection?WT.mc_id=Portal-Microsoft_Azure_ServiceBus
failed (letered) -> https://docs.microsoft.com/es-es/azure/service-bus-messaging/service-bus-dead-letter-queues?WT.mc_id=Portal-Microsoft_Azure_ServiceBus
partitions -> https://docs.microsoft.com/es-es/azure/service-bus-messaging/service-bus-partitioning?WT.mc_id=Portal-Microsoft_Azure_ServiceBus
sessions -> https://docs.microsoft.com/es-es/azure/service-bus-messaging/message-sessions?WT.mc_id=Portal-Microsoft_Azure_ServiceBus
resend -> https://docs.microsoft.com/es-es/azure/service-bus-messaging/service-bus-auto-forwarding?WT.mc_id=Portal-Microsoft_Azure_ServiceBus

-> code:
https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/servicebus/Azure.Messaging.ServiceBus

-> Queues vs topics:
https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview



9. Dependency Injection:
https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-guidelines
https://www.c-sharpcorner.com/UploadFile/3d39b4/constructor-dependency-injection-pattern-implementation-in-c/

TODO:
1. Configure Dependency injection for .Net :
https://docs.microsoft.com/en-us/dotnet/azure/sdk/dependency-injection
https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/servicebus/Azure.Messaging.ServiceBus#aspnet-core


