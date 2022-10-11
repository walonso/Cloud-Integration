using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Services.Integration.Application.Interfaces;
using Services.Integration.Infrastructure.Azure.Authentication;
using System.Text;

namespace Services.Integration.Infrastructure.Azure;

public class StorageAccountIntegration : IBlobStorageService
{
    private IDefaultAzureCredentialAuthentication _defaultAzureCredential;
    public StorageAccountIntegration(IDefaultAzureCredentialAuthentication defaultAzureCredentialAuthentication)
    {
        _defaultAzureCredential = defaultAzureCredentialAuthentication;
    }
    
    public async Task<string> GetBlob(string storageAccountName, string containerName, string blobName)
    {
        var credential = _defaultAzureCredential.GetAuthentication();
        string blobUri = "https://" + storageAccountName + ".blob.core.windows.net";
        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = blobContainer.GetBlobClient(blobName);
        BlobDownloadResult content = await blobClient.DownloadContentAsync();
        string convertedContent = Encoding.UTF8.GetString(content.Content);
        return convertedContent;
    }
}

