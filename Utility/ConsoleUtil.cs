namespace Utility;

public static class ConsoleUtil
{
    public static string AskForLanguageCode(string fileName)
    {
        Console.WriteLine($"In which language code (en,nl etc) is the audio for video: {fileName}? Leave empty to auto detect");
        var languageCode = Console.ReadLine() ?? string.Empty;
        if (languageCode.Length > 2)
        {
            languageCode = string.Empty;
        }
        return languageCode;
    }
}
