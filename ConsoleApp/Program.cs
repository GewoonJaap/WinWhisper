// See https://aka.ms/new-console-template for more information
using WhisperAI;

Console.WriteLine("Hello, World!");

var audioProcessor = new AudioProcessor();
await audioProcessor.ProcessAudio();