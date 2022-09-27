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
        string accessKey = "8eBLODTpk5d7/cRP7fQLr4cxOkNpmYEMBmdFJLlc0a6KRr0haBTHRzj8sLaUND0SSZfM9cVnhhKl+AStKIl2QA==";
        return await _blobStorageService.GetBlob(storageAccountName, blobContainerName, blobName, accessKey);
    }
}
