namespace Utility.FileSystem
{
    public static class FolderManager
    {

        public const string SubtitlesFolder = "Subtitles";
        public const string WavFolder = "RawAudio";
        public const string Models = "Models";
        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}