namespace Utility;

using Data;
using MimeTypes;

public static class VideoFinder
{
    private static List<string> FindVideosInFolder(string folderPath)
    {
        var videoPaths = new List<string>();
        var files = Directory.GetFiles(folderPath);
        foreach (var file in files)
        {
            //check if file is video by metadata
            var mimeType = MimeTypeMap.GetMimeType(Path.GetExtension(file));
            if (mimeType.StartsWith("video"))
            {
                videoPaths.Add(file);
            }
        }

        return videoPaths;
    }

    public static List<VideoToConvertObject> FindVideosBasedOnPath(string inputPath)
    {
        var videosToConvert = new List<VideoToConvertObject>();

        //check if path is a folder or a file dont use foldermanager
        if (Directory.Exists(inputPath))
        {
            var videos = FindVideosInFolder(inputPath);
            videos.ForEach(video =>
            {
                videosToConvert.Add(GetVideoDetails(video));
            });
        }

        else if (File.Exists(inputPath))
        {
            videosToConvert.Add(GetVideoDetails(inputPath));
        }

        else
        {
            Console.WriteLine("The path you entered is not a valid file or folder. Please try again.");
        }
        return videosToConvert;
    }

    private static VideoToConvertObject GetVideoDetails(string videoPath)
    {
        var videoName = Path.GetFileName(videoPath);
        var languageCode = ConsoleUtil.AskForLanguageCode(videoName);
        var shouldTranslate = ConsoleUtil.AskIfNeedsToBeTranslated();
        return new VideoToConvertObject(videoPath, languageCode, shouldTranslate);
    }
}
