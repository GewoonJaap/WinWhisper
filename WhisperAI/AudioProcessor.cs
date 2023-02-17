using Whisper.net;
using Whisper.net.Ggml;

namespace WhisperAI
{
    public class AudioProcessor
    {
        async public Task ProcessAudio()
        {

            var modelName = "ggml-base.bin";
            if (!File.Exists(modelName))
            {
                using var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(GgmlType.Base);
                using var fileWriter = File.OpenWrite(modelName);
                await modelStream.CopyToAsync(fileWriter);
            }

            var builder = WhisperProcessorBuilder.Create()
                .WithFileModel("C:/Users/Jaap/source/repos/WinWhisper/ConsoleApp/bin/Debug/net7.0/ggml-base.bin")
                .WithSegmentEventHandler(OnNewSegment)
                .WithLanguage("auto");
            using var processor = builder.Build();

            void OnNewSegment(object sender, OnSegmentEventArgs e)
            {
                Console.WriteLine($"CSSS {e.Start} ==> {e.End} : {e.Segment}");
            }

            using var fileStream = File.OpenRead("C:/Users/Jaap/Downloads/350dd587e8736453b7770b975979cc5b40d704470259297f93bee626c12b71fb.wav");
            processor.Process(fileStream);
        }
    }
}