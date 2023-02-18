// See https://aka.ms/new-console-template for more information

using AudioExtractor;
using WhisperAI;
Console.Clear();
Console.WriteLine("Hello, World!");
Console.WriteLine("Enter the video path...");
var videoPath = Console.ReadLine() ?? string.Empty;
//if path starts and ends with ", remove
if (videoPath.StartsWith("\"") && videoPath.EndsWith("\""))
{
    videoPath = videoPath[1..^1];
}
var extractor = new Extractor();
//extractor.ExtractAudioFromVideoFile(videoPath);

var audioProcessor = new AudioProcessor();
await audioProcessor.ProcessAudio("output.wav");