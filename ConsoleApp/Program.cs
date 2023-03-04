// See https://aka.ms/new-console-template for more information

using AudioExtractor;
using Sentry;
using Utility;
using WhisperAI;


using (SentrySdk.Init(o =>
       {
           o.Dsn = "https://02ce93a1ef0d4a61b3e6e019e5909617@o252778.ingest.sentry.io/4504780017041408";
           // When configuring for the first time, to see what the SDK is doing:
           o.Debug = true;
           // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
           // We recommend adjusting this value in production.
           o.TracesSampleRate = 1.0;
           // Enable Global Mode if running in a client app
           o.IsGlobalModeEnabled = true;
       }))
{

    //create folder Subtitles if not exists
    FolderManager.CreateFolder(FolderManager.SubtitlesFolder);
    FolderManager.CreateFolder(FolderManager.WavFolder);
    FolderManager.CreateFolder(FolderManager.Models);

    Console.Clear();
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
}
