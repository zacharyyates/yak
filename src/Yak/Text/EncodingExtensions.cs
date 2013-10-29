using System.Text;

namespace Yak.Text {
    public static class EncodingExtensions {
        // adapted from http://www.west-wind.com/weblog/posts/2007/Nov/28/Detecting-Text-Encoding-for-StreamReader
        public static Encoding DetectEncoding(this byte[] buffer) {
            buffer.ThrowIfNull("buffer");

            // Use Default of Encoding.Default (Ansi CodePage)
            var enc = Encoding.Default;

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                enc = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                enc = Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                enc = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                enc = Encoding.UTF7;

            return enc;
        }
    }
}