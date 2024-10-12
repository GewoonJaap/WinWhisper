// See https://aka.ms/new-console-template for more information

using AudioExtractor;
using Data.Model;
using System.Reflection;
using System.Runtime.InteropServices;
using Utility;
using Utility.FileSystem;
using Velopack;
using WhisperAI;


internal class Program
{
    private static async Task Main(string[] args)
    {
        using (SentrySdk.Init(o =>
       {
           o.Dsn = "https://02ce93a1ef0d4a61b3e6e019e5909617@o252778.ingest.sentry.io/4504780017041408";
           o.TracesSampleRate = 1.0;
           o.IsGlobalModeEnabled = true;
       }))
        {

            try
            {
                //only on windows
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    RegisterSquirrel();
                }

                //create folder Subtitles if not exists
                FolderManager.CreateFolder(FolderManager.SubtitlesFolder);
                FolderManager.CreateFolder(FolderManager.WavFolder);
                FolderManager.CreateFolder(FolderManager.Models);

                while (true)
                {
                    var videosToConvert = GatherVideosToConvert();

                    if (videosToConvert.Videos.Count == 0)
                    {
                        Console.WriteLine("No videos found in this path. Please try again.");
                        Console.WriteLine("If you expect WinWhisper to find videos. Please open a bug report on our GitHub page: https://github.com/GewoonJaap/WinWhisper/issues/new?assignees=&labels=&projects=&template=bug_report.md&title=");
                        Console.WriteLine("Press any key to retry...");
                        Console.ReadKey();
                        continue;
                    }


                    await videosToConvert.Videos.ForEachAsync(async video =>
                    {
                        Console.WriteLine($"Processing video: {video.VideoName}");
                        var audioPath = Extractor.ExtractAudioFromVideoFile(video.VideoPath);

                        var audioProcessor = new AudioProcessor(ModelNameFetcher.ModelTypeToGgmlType(videosToConvert.ModelType), videosToConvert.SubtitleOutputPath);
                        await audioProcessor.ProcessAudio(audioPath, video.LanguageCode, video.ShouldTranslate);
                        //remove audioPath file
                        File.Delete(audioPath);
                        Console.WriteLine($"Finished audio processing for video: {video.VideoName}");

                    });


                    Console.WriteLine("Do you want to process another video? (yes/no)");
                    var processAnotherVideo = Console.ReadLine() ?? string.Empty;
                    if (processAnotherVideo.ToLower() == "no" || processAnotherVideo.ToLower() == "n")
                    {
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                ConsoleUtil.LogException(ex);
            }
        }

    }

    private static void RegisterSquirrel()
    {
        if (File.Exists(".isStandalone"))
        {
            return;
        }
        VelopackApp.Build()
            .WithFirstRun(v => Console.WriteLine($"Thanks for installing WinWhisper version {Assembly.GetExecutingAssembly().GetName().Version}"))
            .Run();
        UpdateMyApp();

    }

    private static GatherVideoResult GatherVideosToConvert()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to WinWhisper ({Assembly.GetExecutingAssembly().GetName().Version}). Generate subtitles with ease using WhisperAI.");

        Console.WriteLine("Enter the path where you want the subtitles to be saved...");
        Console.WriteLine("Leave empty to save the subtitles in the ./Subtitles folder");

        var subtitleOutputPath = Console.ReadLine() ?? string.Empty;
        subtitleOutputPath = PathUtil.FormatPath(subtitleOutputPath);

        Console.WriteLine("Enter the video/audio path or the folder path that contains the videos/audios you want to process...");
        var inputPath = Console.ReadLine() ?? string.Empty;
        //if path starts and ends with ", remove
        inputPath = PathUtil.FormatPath(inputPath);
        var modelType = ConsoleUtil.AskForModelType();
        Console.WriteLine($"Selected model: {modelType}");
        var videos = VideoFinder.FindVideosBasedOnPath(inputPath);

        return new GatherVideoResult(videos, subtitleOutputPath, modelType);

    }

    private static async Task UpdateMyApp()
    {
        try
        {
            var mgr = new UpdateManager("https://winwhisper.ams3.digitaloceanspaces.com");

            if(mgr.IsInstalled == false)
            {
                return;
            }

            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null)
            {
                return;
            }

            if (newVersion != null)
            {
                await mgr.DownloadUpdatesAsync(newVersion);
                Console.WriteLine($"WinWhisper update {newVersion.TargetFullRelease.Version} is available! Installing on exit.");
                mgr.ApplyUpdatesAndRestart(newVersion);
            }
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);
        }
    }
}