namespace Data;

public class VideoToConvertObject
{
    public string VideoPath { get; }
    public string LanguageCode { get; }

    public string VideoName { get; }

    public VideoToConvertObject(string videoPath, string languageCode)
    {
        VideoPath = videoPath;
        LanguageCode = languageCode;
        VideoName = Path.GetFileName(videoPath);
    }
}
