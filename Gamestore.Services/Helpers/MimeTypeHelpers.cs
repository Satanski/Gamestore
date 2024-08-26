namespace Gamestore.BLL.Helpers;

public static class MimeTypeHelpers
{
    private static readonly Dictionary<byte[], string> _knownHeaders = new Dictionary<byte[], string>
    {
        { new byte[] { 0xFF, 0xD8, 0xFF }, "image/jpeg" },
        { new byte[] { 0x89, 0x50, 0x4E, 0x47 }, "image/png" },
        { "GIF"u8.ToArray(), "image/gif" },
        { "RIFF"u8.ToArray(), "image/webp" },
    };

    public static string GetMimeTypeFromBytes(byte[] fileData)
    {
        var mimeTypeKV = _knownHeaders.FirstOrDefault(kvp => StartsWith(fileData, kvp.Key));
        if (!string.IsNullOrEmpty(mimeTypeKV.Value))
        {
            return mimeTypeKV.Value;
        }

        return "application/octet-stream";
    }

    private static bool StartsWith(byte[] fileData, byte[] header)
    {
        if (fileData.Length < header.Length)
        {
            return false;
        }

        for (int i = 0; i < header.Length; i++)
        {
            if (fileData[i] != header[i])
            {
                return false;
            }
        }

        return true;
    }
}
