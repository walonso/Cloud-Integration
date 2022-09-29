using Microsoft.AspNetCore.Mvc;
using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Config;

namespace Services.Integration.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BlobStorageController : ControllerBase
{

    private readonly ILogger<BlobStorageController> _logger;
    private IBlobStorageService _blobStorageService;

    public BlobStorageController(ILogger<BlobStorageController> logger, IBlobStorageService blobStorageService)
    {
        _logger = logger;
        _blobStorageService = blobStorageService;
    }

    [HttpGet(Name = "GetBlob")]
    public async Task<string> Get(string storageAccountName, string blobContainerName, string blobName)
    {
        string accessKey = "zyXuFB5hlE8eN9NLMtQk2wurY5CSQyvNvAJx8eolS/VZv+7RoCuEVHwnH1w6i8QYSmWjlrmcJ+9s+AStNO0zug==";
        return await _blobStorageService.GetBlob(storageAccountName, blobContainerName, blobName, accessKey);
    }
}
