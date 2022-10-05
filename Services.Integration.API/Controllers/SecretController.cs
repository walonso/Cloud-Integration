using Microsoft.AspNetCore.Mvc;
using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Config;

namespace Services.Integration.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SecretController : ControllerBase
{

    private readonly ILogger<SecretController> _logger;
    private ISecretStorageService _secretStorageService;

    public SecretController(ILogger<SecretController> logger, ISecretStorageService secretStorageService)
    {
        _logger = logger;
        _secretStorageService=secretStorageService;
    }

    [HttpGet(Name = "GetSecret")]
    public async Task<string> Get(string secretName)
    {
        SecretStorageConfig config = new SecretStorageConfig();
        config.NameStorageSecret= "bootcampcloudkv2";

        return await _secretStorageService.GetSecretAsync(secretName);                
    }
}
