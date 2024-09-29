using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class GatherVideoResult(List<VideoToConvertObject> videos, string subtitleOutputPath, WinWhisperModelType modelType)
    {
        public List<VideoToConvertObject> Videos { get; } = videos;
        public string SubtitleOutputPath { get; } = subtitleOutputPath;
        public WinWhisperModelType ModelType { get; } = modelType;
    }
}
