namespace Gamestore.MongoRepository.Helpers;

public static class GuidHelpers
{
    public static Guid IntToGuid(int value)
    {
        var guidBytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(guidBytes, 0);
        return new Guid(guidBytes);
    }

    public static int GuidToInt(Guid value)
    {
        var guidBytes = value.ToByteArray();
        return BitConverter.ToInt32(guidBytes, 0);
    }
}
