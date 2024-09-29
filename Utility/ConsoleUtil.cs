using Data.Enum;
using Whisper.net.Ggml;

namespace Utility;

public static class ConsoleUtil
{
    private const string DEFAULT_MODEL_TYPE = "base";
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

    public static WinWhisperModelType AskForModelType()
    {
        Console.WriteLine("Which model type do you want to use? (base, base-en, large-v2, large-v1, large-v3, medium, medium-en, small, small-en, tiny, tiny-en)");
        Console.WriteLine($"Default: {DEFAULT_MODEL_TYPE}");
        var modelType = Console.ReadLine() ?? DEFAULT_MODEL_TYPE;
        if(modelType.Trim().Length == 0)
        {
            modelType = DEFAULT_MODEL_TYPE;
        }
        try
        {
            return ModelNameFetcher.StringToModelType(modelType.ToLower());
        }
        catch
        {
            Console.WriteLine($"Invalid model type. Defaulting to {DEFAULT_MODEL_TYPE} model.");
            return ModelNameFetcher.StringToModelType(DEFAULT_MODEL_TYPE);
        }
    }

    public static bool AskIfNeedsToBeTranslated()
    {
        Console.WriteLine("Do you want to translate the subtitles to English? (yes/no) Default: no");
        var translateSubtitles = Console.ReadLine() ?? string.Empty;
        return translateSubtitles.Equals("yes", StringComparison.CurrentCultureIgnoreCase) || translateSubtitles.Equals("y", StringComparison.CurrentCultureIgnoreCase);
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
