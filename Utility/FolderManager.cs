namespace Utility
{
    public static class FolderManager
    {

        public static string subtitlesFolder = "Subtitles";
        public static string wavFolder = "rawAudio";
        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}