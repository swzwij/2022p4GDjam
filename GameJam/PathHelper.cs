using System.IO;

namespace Eindopdracht
{
    public static class PathHelper
    {
        public static string ExeDir()
        {
            return new FileInfo(typeof(PathHelper).Assembly.Location).DirectoryName;
        }
    }
}



