using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;
using Services.Integration.Application.Interfaces;

namespace Services.Integration.Infrastructure.Azure;

public class ServiceBusQueueIntegration : IMessageQueueService
{
    private ServiceBusClient _client;
    public ServiceBusQueueIntegration(string connectionString)
    {
        // set the transport type to AmqpWebSockets so that the ServiceBusClient uses the port 443. 
        // If you use the default AmqpTcp, you will need to make sure that the ports 5671 and 5672 are open
        var clientOptions = new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpWebSockets };
        _client = new ServiceBusClient(connectionString, clientOptions);
    }

    public async Task SendMessageQueueAsync(string queueName, string message)
    {
        // create the sender
        ServiceBusSender sender = _client.CreateSender(queueName);
        
        // create a message that we can send. UTF-8 encoding is used when providing a string.
        ServiceBusMessage busMessage = new ServiceBusMessage(message);

        // send the message
        await sender.SendMessageAsync(busMessage);
    }

    public async Task<string> ReceiveMessageQueueAsync(string queueName, bool completeMessage)
    {
        // create a receiver that we can use to receive the message
        ServiceBusReceiver receiver = _client.CreateReceiver(queueName);

        // the received message is a different type as it contains some service set properties
        ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

        if (completeMessage) { 
            await receiver.CompleteMessageAsync(receivedMessage);
        }

        // get the message body as a string
        string body = receivedMessage.Body.ToString();
        return body;
    }

    public async Task SendMessageBathQueueAsync(string queueName)
    {
        // create the sender
        ServiceBusSender sender = _client.CreateSender(queueName);
        
        IList<ServiceBusMessage> messages = new List<ServiceBusMessage>();
        messages.Add(new ServiceBusMessage("First"));
        messages.Add(new ServiceBusMessage("Second"));
        // send the messages
        await sender.SendMessagesAsync(messages);
    }

    public async Task<string> ReceiveMessageBathQueueAsync(string queueName, bool completeMessage)
    {
        // create a receiver that we can use to receive the messages
        ServiceBusReceiver receiver = _client.CreateReceiver(queueName);

        // the received message is a different type as it contains some service set properties
        // a batch of messages (maximum of 2 in this case) are received
        IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(maxMessages: 2);

        string body= string.Empty;
        // go through each of the messages received
        foreach (ServiceBusReceivedMessage receivedMessage in receivedMessages)
        {
            // get the message body as a string
            body = receivedMessage.Body.ToString();
        }

        return body;
        // the received message is a different type as it contains some service set properties
        //ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

        // complete the message, thereby deleting it from the service
        //await receiver.CompleteMessageAsync(receivedMessage);
    }
}