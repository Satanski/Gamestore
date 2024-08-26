using Azure.Identity;
using Azure.Storage.Blobs;

namespace Gamestore.BLL.Azure;

public static class BlobService
{
    public static BlobServiceClient GetBlobServiceClient(string accountName)
    {
        var uri = new Uri($"https://{accountName}.blob.core.windows.net");
        BlobServiceClient client = new(
            uri,
            new DefaultAzureCredential());

        return client;
    }

    public static BlobContainerClient GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
    {
        // Create the container client using the service client object
        BlobContainerClient client = blobServiceClient.GetBlobContainerClient(containerName);
        return client;
    }

    public static BlobContainerClient GetBlobContainerClient(string accountName, string containerName, BlobClientOptions clientOptions)
    {
        BlobContainerClient client = new(
            new Uri($"https://{accountName}.blob.core.windows.net/{containerName}"),
            new DefaultAzureCredential(),
            clientOptions);

        return client;
    }
}
