using Microsoft.AspNetCore.Mvc;
using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Config;

namespace Services.Integration.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageQueueController : ControllerBase
{

    private readonly ILogger<MessageQueueController> _logger;
    private IMessageQueueService _messageQueueService;

    public MessageQueueController(ILogger<MessageQueueController> logger, IMessageQueueService messageQueueService)
    {
        _logger = logger;
        _messageQueueService=messageQueueService;
    }

    [HttpGet(Name = "GetMessage")]
    public async Task<string> GetMessage(string queueName, bool completeMessage)
    {
        return await _messageQueueService.ReceiveMessageQueueAsync(queueName,completeMessage);                
    }

    [HttpPost("SetMessage")]
    public async Task<bool> Set(string queueName, string message)
    {    
        await _messageQueueService.SendMessageQueueAsync(queueName, message);  
        return true;         
    }
}
