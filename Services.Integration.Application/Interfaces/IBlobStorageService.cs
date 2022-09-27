using System;
using System.Collections.Generic;
namespace Services.Integration.Application.Interfaces;

public interface IBlobStorageService
{
    Task<string> GetBlob(string storageAccountName, string containerName, string blobName, string accessKey);
}

