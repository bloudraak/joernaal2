using System.IO;
using System.Text;

namespace Joernaal
{
    public class Item
    {
        public Item(string path, string baseDirectory)
        {
            SourcePath = Path.GetFullPath(path);
            Key = path;

            if (!baseDirectory.EndsWith("\\"))
            {
                baseDirectory += "\\";
            }
            if (Key.StartsWith(baseDirectory))
            {
                Key = Key.Substring(baseDirectory.Length);
            }

            TargetPath = Key;
            Contents = File.ReadAllBytes(path);
        }

        public string SourcePath { get; set; }
        public string TargetPath { get; set; }

        public string Key { get; set; }
        public byte[] Contents { get; set; }

        public string Extension
        {
            get { return Path.GetExtension(TargetPath); }
        }

        public void SetContents(string contents)
        {
            using (var stream = new MemoryStream(1024*64))
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(contents);
                }
                Contents = stream.ToArray();
            }
        }

        public string ContentsAsText()
        {
            string contents;
            using (var stream = new MemoryStream(Contents))
            {
                using (var reader = new StreamReader(stream))
                {
                    contents = reader.ReadToEnd();
                }
            }
            return contents;
        }
    }
}