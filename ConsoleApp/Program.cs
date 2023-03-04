// See https://aka.ms/new-console-template for more information

using AudioExtractor;
using Utility;
using WhisperAI;


//create folder Subtitles if not exists
FolderManager.CreateFolder(FolderManager.subtitlesFolder);
FolderManager.CreateFolder(FolderManager.wavFolder);

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

var audioPath= Extractor.ExtractAudioFromVideoFile(videoPath);

var audioProcessor = new AudioProcessor();
await audioProcessor.ProcessAudio(audioPath, languageCode);
//remove audioPath file
File.Delete(audioPath);
Console.WriteLine("Finished audio processing");