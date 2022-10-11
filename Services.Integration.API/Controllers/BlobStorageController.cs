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
        return await _blobStorageService.GetBlob(storageAccountName, blobContainerName, blobName);
    }
}
