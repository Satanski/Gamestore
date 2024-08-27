namespace Gamestore.BLL.Azure;

public interface IPicturesBlobService
{
    Task UploadPictureAsync(string stringifiedImage, string fileName);

    Task DeletePictureAsync(string fileName);

    Task<(byte[] ImageBytes, string MimeType)> DownloadPictureAsync(string fileName);

    Task UpdatePictureAsync(string stringifiedImage, string fileName);
}
