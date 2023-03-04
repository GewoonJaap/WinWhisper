// See https://aka.ms/new-console-template for more information

using AudioExtractor;
using WhisperAI;


//create folder Subtitles if not exists
var subtitlesFolder = "Subtitles";
if (!Directory.Exists(subtitlesFolder))
{
    Directory.CreateDirectory(subtitlesFolder);
}
//create wav folder if not exists
var wavFolder = "rawAudio";
if (!Directory.Exists(wavFolder))
{
    Directory.CreateDirectory(wavFolder);
}

Console.Clear();
Console.WriteLine("Enter the video path...");
var videoPath = Console.ReadLine() ?? string.Empty;
//if path starts and ends with ", remove
if (videoPath.StartsWith("\"") && videoPath.EndsWith("\""))
{
    videoPath = videoPath[1..^1];
}
var extractor = new Extractor();
var audioPath= extractor.ExtractAudioFromVideoFile(videoPath);

var audioProcessor = new AudioProcessor();
await audioProcessor.ProcessAudio(audioPath);
//remove audioPath file
File.Delete(audioPath);
Console.WriteLine("Finished audio processing");