using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.FileSystem
{
    public static class PathUtil
    {

        public static string FormatPath(string path)
        {
            //strip first and last " if exists
            if (path.StartsWith("\""))
                path = path.Substring(1);
            if (path.EndsWith("\""))
                path = path.Substring(0, path.Length - 1);
            //strip last \ if exists
            if (path.EndsWith("\\"))
                path = path.Substring(0, path.Length - 1);
            return path;
        }
    }
}
