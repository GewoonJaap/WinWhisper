using Whisper.net;
using Whisper.net.Ggml;

namespace WhisperAI
{
    public class AudioProcessor
    {
        private List<OnSegmentEventArgs> segments = new();
        public async Task ProcessAudio(string wavPath)
        {
            segments.Clear();
            const string modelName = "ggml-base.bin";
            if (!File.Exists(modelName))
            {
                Console.WriteLine("Download Whisper AI model. This might take a while depending on your internet speed..");
                using var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(GgmlType.Base);
                using var fileWriter = File.OpenWrite(modelName);
                await modelStream.CopyToAsync(fileWriter);
            }

            var builder = WhisperProcessorBuilder.Create()
                .WithFileModel(modelName)
                .WithSegmentEventHandler(OnNewSegment)
                .WithLanguage("en");
            using var processor = builder.Build();

            void OnNewSegment(object sender, OnSegmentEventArgs e)
            {
                Console.WriteLine($"CSSS {e.Start} ==> {e.End} : {e.Segment}");
                segments.Add(e);
            }

            await using var fileStream = File.OpenRead(wavPath);
            Console.WriteLine("Processing audio file");
            processor.Process(fileStream);
            Console.WriteLine("Processed");
            processor.Dispose();
            //Sort segments by start time
            segments.Sort((x, y) => x.Start.CompareTo(y.Start));
            //Write segments to file as subtitles
            using var writer = new StreamWriter("output.srt");
            for (var i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                writer.WriteLine(i + 1);
                writer.WriteLine($"{segment.Start} --> {segment.End}");
                writer.WriteLine(segment.Segment);
                writer.WriteLine();
            }
            
        }
    }
}