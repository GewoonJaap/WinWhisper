using Utility;
using Whisper.net;
using Whisper.net.Ggml;

namespace WhisperAI
{
    public class AudioProcessor
    {
        private List<SegmentData> _segments = new();
        private const GgmlType ModelType = GgmlType.Base;

        public async Task ProcessAudio(string wavPath, string languageCode)
        {
            var modelName = ModelNameFetcher.GgmlTypeToString(ModelType);
            var modelPath = FolderManager.Models + "/" + modelName;
            _segments.Clear();
            if (!File.Exists(modelPath))
            {
                Console.WriteLine($"Downloading Whisper AI model {modelName}. This might take a while depending on your internet speed..");
                Console.WriteLine("The application might exit after downloading. Please restart the application manually in that case!");
                var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(ModelType);
                var fileWriter = File.OpenWrite(modelPath);
                await modelStream.CopyToAsync(fileWriter);
            }
            
            var whisperFactory = WhisperFactory.FromPath(modelPath);

            var builder = whisperFactory.CreateBuilder()
                .WithSegmentEventHandler(OnNewSegment);

            if (languageCode.Length > 0)
            {
                builder.WithLanguage(languageCode);
            }
            else
            {
                builder.WithLanguageDetection();
            }
            var processor = builder.Build();

            void OnNewSegment(SegmentData segmentData)
            {
                var startTime = TimestampConverter.ConvertTimestampToSrtFormat(segmentData.Start);
                var endTime = TimestampConverter.ConvertTimestampToSrtFormat(segmentData.End);
                Console.WriteLine($"CSSS {startTime} ==> {endTime} : {segmentData.Text}");
                _segments.Add(segmentData);
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
            _segments.Sort((x, y) => x.Start.CompareTo(y.Start));
            //remove all segments that have same time + content
            _segments = _segments.Distinct().ToList();
            //Write segments to file as subtitles
            var outputFilePath = GetOutputFilePath(wavPath);
            
            var writer = new StreamWriter(outputFilePath);
            for (var i = 0; i < _segments.Count; i++)
            {
                var segment = _segments[i];
                writer.WriteLine(i + 1);
                //segment.Start is now in the following format: 00:00:03.0200000, but should be 00:00:03,020
                var startTime = TimestampConverter.ConvertTimestampToSrtFormat(segment.Start);
                var endTime = TimestampConverter.ConvertTimestampToSrtFormat(segment.End);
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

        private static string GetOutputFilePath(string wavPath)
        {
            var fileName = Path.GetFileNameWithoutExtension(wavPath);
            return FolderManager.SubtitlesFolder + "/" + fileName + ".srt";
        }
    }
}