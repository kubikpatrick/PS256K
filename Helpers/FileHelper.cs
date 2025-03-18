namespace PS256K.Helpers;

public static class FileHelper
{
    public static string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }

    public static string GetExtension(string path)
    {
        return Path.GetExtension(path);
    }

    public static void Delete(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        File.Delete(path);
    }
}