using NAudio.Wave;
using System.Reflection.PortableExecutable;

namespace AudioExtractor
{
    public class Extractor
    {
        public void ExtractAudioFromVideoFile(string videoFilePath)
        {
            Console.WriteLine("Extracting audio from video file located at: " + videoFilePath);
            Console.WriteLine("This might take a while depending on the file size and drive speed...");
            const int outRate = 16000;
            var fileStream = File.OpenRead(videoFilePath);
            //filestream to memorystream
            var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            fileStream.Close();
            var reader = new StreamMediaFoundationReader(memoryStream);
            var outFormat = new WaveFormat(outRate, reader.WaveFormat.Channels);
            var resampler = new MediaFoundationResampler(reader, outFormat);

            WaveFileWriter.CreateWaveFile("output.wav", resampler);
            Console.WriteLine("Extracted audio from video, output.wav");
        }
    }
}