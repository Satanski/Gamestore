using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Gamestore.BLL.Azure;

public class PicturesBlobService(IConfiguration configuration) : IPicturesBlobService
{
    public async Task<(byte[] ImageBytes, string MimeType)> DownloadPictureAsync(string fileName)
    {
        byte[] imageBytes;
        var blobClient = GetPicturesBlobClient(fileName, configuration);
        using (MemoryStream blobStream = new MemoryStream())
        {
            await blobClient.DownloadToAsync(blobStream);

            imageBytes = blobStream.ToArray();
        }

        var blobProperties = await blobClient.GetPropertiesAsync();
        var mimeType = blobProperties.Value.ContentType;

        return (imageBytes, mimeType);
    }

    public async Task UploadPictureAsync(string stringifiedImage, string fileName)
    {
        var img = CreateUploadableImage(stringifiedImage);

        BlobClient blobClient = GetPicturesBlobClient(fileName, configuration);

        using MemoryStream uploadFileStream = new MemoryStream(img);
        await blobClient.UploadAsync(uploadFileStream);
        await SetFileMimeTypeAsync(stringifiedImage, blobClient);
    }

    public async Task UpdatePictureAsync(string stringifiedImage, string fileName)
    {
        var img = CreateUploadableImage(stringifiedImage);

        BlobClient blobClient = GetPicturesBlobClient(fileName!, configuration);

        using MemoryStream uploadFileStream = new MemoryStream(img);
        await blobClient.UploadAsync(uploadFileStream, overwrite: true);
        await SetFileMimeTypeAsync(stringifiedImage, blobClient);
    }

    public async Task DeletePictureAsync(string fileName)
    {
        BlobClient blobClient = GetPicturesBlobClient(fileName!, configuration);

        await blobClient.DeleteIfExistsAsync();
    }

    private static byte[] CreateUploadableImage(string stringifiedImage)
    {
        return Convert.FromBase64String(stringifiedImage[(stringifiedImage.IndexOf(',') + 1)..]);
    }

    private static BlobContainerClient GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
    {
        BlobContainerClient client = blobServiceClient.GetBlobContainerClient(containerName);
        return client;
    }

    private static BlobClient GetPicturesBlobClient(string fileName, IConfiguration configuration)
    {
        string connectionString = configuration["Azure:BlobConnectionString"];
        string containerName = configuration["Azure:PicturesContainer"];

        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        var blobContainerClient = GetBlobContainerClient(blobServiceClient, containerName!);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        return blobClient;
    }

    private static async Task SetFileMimeTypeAsync(string stringifiedImage, BlobClient blobClient)
    {
        string mimeType = stringifiedImage.Split(';')[0].Remove(0, 5);
        BlobHttpHeaders headers = new BlobHttpHeaders
        {
            ContentType = mimeType,
        };
        await blobClient.SetHttpHeadersAsync(headers);
    }
}
