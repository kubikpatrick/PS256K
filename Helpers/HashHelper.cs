using System.Security.Cryptography;

namespace PS256K.Helpers;

public static class HashHelper
{
    public readonly static SHA256 SHA256;

    static HashHelper()
    {
        SHA256 = SHA256.Create();
    }

    public static string ComputeHashFile(FileStream stream)
    {
        byte[] hash = SHA256.ComputeHash(stream);

        return Convert.ToHexString(hash);
    }
}