using NAudio.Wave;
using System.Reflection.PortableExecutable;
using Utility;

namespace AudioExtractor
{
    public class Extractor
    {
        public static string ExtractAudioFromVideoFile(string videoFilePath)
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

            //get file name from videoFilePath, remove extension
            var outputFilePath = GetOutputFolder(videoFilePath);

            WaveFileWriter.CreateWaveFile(outputFilePath, resampler);
            Console.WriteLine("Extracted audio from video, " + outputFilePath);
            return outputFilePath;
        }

        private static string GetOutputFolder(string videoFilePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(videoFilePath);
            return FolderManager.WavFolder + "/" + fileName + ".wav";
        }
    }
}