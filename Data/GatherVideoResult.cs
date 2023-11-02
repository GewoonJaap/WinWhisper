using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class GatherVideoResult
    {
        public List<VideoToConvertObject> Videos { get;}
        public string SubtitleOutputPath { get; }

        public GatherVideoResult(List<VideoToConvertObject> videos, string subtitleOutputPath)
        {
            Videos = videos;
            SubtitleOutputPath = subtitleOutputPath;
        }
    }
}
