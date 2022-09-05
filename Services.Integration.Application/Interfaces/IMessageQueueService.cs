namespace Services.Integration.Application.Interfaces;

public interface IMessageQueueService {
    Task SendMessageQueueAsync(string queueName, string message);
    Task<string> ReceiveMessageQueueAsync(string queueName, bool completeMessage);
    Task SendMessageBathQueueAsync(string queueName);
    Task<string> ReceiveMessageBathQueueAsync(string queueName, bool completeMessage);
}