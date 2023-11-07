namespace Data;

public class VideoToConvertObject
{
    public string VideoPath { get; }
    public string LanguageCode { get; }

    public string VideoName { get; }
    public bool ShouldTranslate { get; }

    public VideoToConvertObject(string videoPath, string languageCode, bool shouldTranslate = false)
    {
        VideoPath = videoPath;
        LanguageCode = languageCode;
        VideoName = Path.GetFileName(videoPath);
        ShouldTranslate = shouldTranslate;
    }
}
