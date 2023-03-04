namespace Utility
{
    public static class FolderManager
    {

        public static string SubtitlesFolder = "Subtitles";
        public static string WavFolder = "RawAudio";
        public static string Models = "Models";
        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}