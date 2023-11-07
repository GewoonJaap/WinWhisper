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

    public static bool AskIfNeedsToBeTranslated()
    {
        Console.WriteLine("Do you want to translate the subtitles to English? (yes/no) Default: no");
        var translateSubtitles = Console.ReadLine() ?? string.Empty;
        return translateSubtitles.ToLower() == "yes" || translateSubtitles.ToLower() == "y";
    }

    public static void LogException(Exception ex)
    {
        Console.WriteLine("An error occured. Please report the following on our GitHub page: https://github.com/GewoonJaap/WinWhisper/issues/new?assignees=&labels=&projects=&template=bug_report.md&title=");
        Console.WriteLine("Error details:");
        Console.WriteLine("========== Start Of Error ==========");
        Console.WriteLine("Error name:");
        Console.WriteLine(ex.GetType()?.Name);
        Console.WriteLine("Error message:");
        Console.WriteLine(ex.Message);
        Console.WriteLine("Error stacktrace:");
        Console.WriteLine(ex.StackTrace);
        Console.WriteLine("Error inner exception:");
        Console.WriteLine(ex.InnerException?.Message);
        Console.WriteLine("Error inner exception stacktrace:");
        Console.WriteLine(ex.InnerException?.StackTrace);
        Console.WriteLine("========== End Of Error ==========");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
