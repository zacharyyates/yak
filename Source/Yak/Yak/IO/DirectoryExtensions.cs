using System.IO;

namespace Yak.IO
{
    public static class DirectoryExtensions
    {
        public static void EnsureDirectory(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }
}