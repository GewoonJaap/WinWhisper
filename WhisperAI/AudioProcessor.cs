using Whisper.net;
using Whisper.net.Ggml;

namespace WhisperAI
{
    public class AudioProcessor
    {
        private List<SegmentData> segments = new();
        public async Task ProcessAudio(string wavPath)
        {
            segments.Clear();
            const string modelName = "ggml-base.bin";
            if (!File.Exists(modelName))
            {
                Console.WriteLine("Download Whisper AI model. This might take a while depending on your internet speed..");
                var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(GgmlType.Base);
                var fileWriter = File.OpenWrite(modelName);
                await modelStream.CopyToAsync(fileWriter);
            }
            
            var whisperFactory = WhisperFactory.FromPath("ggml-base.bin");

            var builder = whisperFactory.CreateBuilder()
                .WithSegmentEventHandler(OnNewSegment)
                .WithLanguage("en");
            var processor = builder.Build();

            void OnNewSegment(SegmentData segmentData)
            {
                Console.WriteLine($"CSSS {segmentData.Start} ==> {segmentData.End} : {segmentData.Text}");
                segments.Add(segmentData);
            }

            //Read entire file from path, put in stream.

            using (Stream fileStream = File.OpenRead(wavPath))
            {
                GC.KeepAlive(fileStream);
                GC.KeepAlive(processor);
                Console.WriteLine("Processing audio file");
                processor.Process(fileStream);
                Console.WriteLine("Processed");
                processor.Dispose();
            }
            //Sort segments by start time
            segments.Sort((x, y) => x.Start.CompareTo(y.Start));
            //Write segments to file as subtitles
            var writer = new StreamWriter("output.srt");
            for (var i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                writer.WriteLine(i + 1);
                writer.WriteLine($"{segment.Start} --> {segment.End}");
                writer.WriteLine(segment.Text);
                writer.WriteLine();
            }
            
        }
    }
}