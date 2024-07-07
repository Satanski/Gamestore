namespace Gamestore.BLL.Helpers;

internal static class GuidHelpers
{
    public static Guid IntToGuid(int value)
    {
        byte[] guidBytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(guidBytes, 0);
        return new Guid(guidBytes);
    }
}
