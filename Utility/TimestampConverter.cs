namespace Utility;

public static class TimestampConverter
{
    public static string ConvertTimestampToSrtFormat(TimeSpan timestamp)
    {
        return timestamp.ToString("hh\\:mm\\:ss\\,fff").Replace(".", ",");
    }
}