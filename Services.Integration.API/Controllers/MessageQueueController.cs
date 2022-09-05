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
    public async Task<string> GetMessage()
    {
        string queueName="myqueue";
        bool completeMessage=true;
        return await _messageQueueService.ReceiveMessageQueueAsync(queueName,completeMessage);                
    }

//TODO: Change to post
    [HttpGet("SetMessage/{message}")]
    public async Task<bool> Get(string message)
    {
        string queueName="myqueue";        
        await _messageQueueService.SendMessageQueueAsync(queueName, message);  
        return true;         
    }
}
