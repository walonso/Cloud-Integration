using Services.Integration.Application.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage;
using Azure.Storage.Blobs.Models;
using System.Text;
using System;

namespace Services.Integration.Infrastructure.Azure;

public class StorageAccountIntegration : IBlobStorageService
{
    public async Task<string> GetBlob(string storageAccountName, string containerName, string blobName, string accessKey)
    {
        StorageSharedKeyCredential sharedKeyCredential =
        new StorageSharedKeyCredential(storageAccountName, accessKey);

        string blobUri = "https://" + storageAccountName + ".blob.core.windows.net";

        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobUri), sharedKeyCredential);
        BlobContainerClient blobContainer= blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient= blobContainer.GetBlobClient(blobName);
        BlobDownloadResult content = await blobClient.DownloadContentAsync();
        string convertedContent = Encoding.UTF8.GetString(content.Content);
        return convertedContent;
    }
}

