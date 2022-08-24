using Microsoft.AspNetCore.Mvc;
using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Config;

namespace Services.Integration.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CacheDatabaseController : ControllerBase
{

    private readonly ILogger<CacheDatabaseController> _logger;
    private ICacheDatabaseService _cacheDatabaseService;

    public CacheDatabaseController(ILogger<CacheDatabaseController> logger, ICacheDatabaseService cacheDatabaseService)
    {
        _logger = logger;
        _cacheDatabaseService=cacheDatabaseService;
    }

    [HttpGet(Name = "GetValue")]
    public async Task<string> Get(string key)
    {
        return await _cacheDatabaseService.GetValueAsync(key);                
    }

//TODO: Change to post
    [HttpGet("{key}/{value}")]
    public async Task<bool> Get(string key, string value)
    {
        return await _cacheDatabaseService.SetValueAsync(key, value);                
    }
}
