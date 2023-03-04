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
                var startTime = segmentData.Start.ToString("hh\\:mm\\:ss\\,fff").Replace(".", ",");
                var endTime = segmentData.End.ToString("hh\\:mm\\:ss\\,fff").Replace(".", ",");
                Console.WriteLine($"CSSS {startTime} ==> {endTime} : {segmentData.Text}");
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
            //remove all segments that have same time + content
            segments = segments.Distinct().ToList();
            //Write segments to file as subtitles
            var fileName = Path.GetFileNameWithoutExtension(wavPath);
            var outputFilePath = Path.Combine(Path.GetDirectoryName(wavPath) ?? "output", fileName + ".srt");
            
            var writer = new StreamWriter(outputFilePath);
            for (var i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                writer.WriteLine(i + 1);
                //segment.Start is now in the following format: 00:00:03.0200000, but should be 00:00:03,020
                var startTime = segment.Start.ToString("hh\\:mm\\:ss\\,fff").Replace(".", ",");
                var endTime = segment.End.ToString("hh\\:mm\\:ss\\,fff").Replace(".", ",");
                writer.WriteLine($"{startTime} --> {endTime}");
                //break the text in max 42 characters, but split only on white space
                var parts = new List<string>();
                var index = 0;

                while (index < segment.Text.Length)
                {
                    if (segment.Text.Length - index <= 42)
                    {
                        parts.Add(segment.Text[index..]);
                        break;
                    }

                    var lastSpace = segment.Text.Substring(index, 42).LastIndexOf(' ');
                    parts.Add(segment.Text.Substring(index, lastSpace));
                    index += lastSpace + 1;
                }

                foreach (string part in parts)
                {
                    writer.WriteLine(part.Trim());
                }

                writer.WriteLine();
            }
            writer.Close();
            
            Console.WriteLine($"Subtitle written to {outputFilePath}");
        }
    }
}