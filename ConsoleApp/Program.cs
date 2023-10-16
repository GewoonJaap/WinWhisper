// See https://aka.ms/new-console-template for more information

using AudioExtractor;
using Sentry;
using Utility;
using WhisperAI;


using (SentrySdk.Init(o =>
       {
           o.Dsn = "https://02ce93a1ef0d4a61b3e6e019e5909617@o252778.ingest.sentry.io/4504780017041408";
           o.TracesSampleRate = 1.0;
           o.IsGlobalModeEnabled = true;
       }))
{

    try { 

    //create folder Subtitles if not exists
    FolderManager.CreateFolder(FolderManager.SubtitlesFolder);
    FolderManager.CreateFolder(FolderManager.WavFolder);
    FolderManager.CreateFolder(FolderManager.Models);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to WinWhisper. Generate subtitles with ease using WhisperAI.");
            Console.WriteLine("Enter the video path...");
            var videoPath = Console.ReadLine() ?? string.Empty;
            //if path starts and ends with ", remove
            videoPath = VideoPathFormatter.formatVideoPath(videoPath);

            Console.WriteLine("In which language code (en,nl etc) is the audio? Leave empty to auto detect");
            var languageCode = Console.ReadLine() ?? string.Empty;
            if (languageCode.Length > 2)
            {
                languageCode = string.Empty;
            }

            var audioPath = Extractor.ExtractAudioFromVideoFile(videoPath);

            var audioProcessor = new AudioProcessor();
            await audioProcessor.ProcessAudio(audioPath, languageCode);
            //remove audioPath file
            File.Delete(audioPath);
            Console.WriteLine("Finished audio processing");

            Console.WriteLine("Do you want to process another video? (yes/no)");
            var processAnotherVideo = Console.ReadLine() ?? string.Empty;
            if (processAnotherVideo.ToLower() == "no" || processAnotherVideo.ToLower() == "n")
            {
                break;
            }
        }

    }
    catch(Exception ex)
    {
        SentrySdk.CaptureException(ex);
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
