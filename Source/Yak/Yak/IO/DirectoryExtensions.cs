using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public static string EscapeWindowsDirectoryPath(string path)
        {
            // fix the windows reserved directory names
            var reserved = new List<string> { 
				"con", "prn", "aux", "nul", "com1", "com2", "com3", "com4", "com5", "com6", "com7", 
				"com8", "com9", "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9" 
			};

            var output = new StringBuilder(path);

            var pathParts = path.Split(StringSplitOptions.RemoveEmptyEntries, '/', '\\');
            foreach (var part in pathParts)
            {
                if (reserved.Contains(part))
                {
                    output = output.Replace(part, "_" + part);
                }
            }

            return output.ToString();
        }
    }
}