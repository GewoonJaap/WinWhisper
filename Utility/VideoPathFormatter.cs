namespace Utility;

public static class VideoPathFormatter
{
    public static string formatVideoPath(string rawVideoPath)
    {
        if (rawVideoPath.StartsWith("\"") && rawVideoPath.EndsWith("\""))
        {
           return rawVideoPath[1..^1];
        }

        return rawVideoPath;
    }
}