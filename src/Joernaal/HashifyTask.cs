using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Joernaal
{
    public class HashifyTask : Task
    {
        protected override bool ShouldExecute(Item item, TaskContext taskContext)
        {
            switch (item.Extension)
            {
                case ".css":
                case ".js":
                case ".jpg":
                case ".jpeg":
                case ".svg":
                case ".gif":
                    return true;
            }
            return false;
        }

        protected override void Execute(Item item, TaskContext context)
        {
            using (var algorithm = MD5.Create())
            {
                var data = algorithm.ComputeHash(item.Contents);
                var hash = BaseEncoder.Encode(data);
                var directoryName = Path.GetDirectoryName(item.TargetPath);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.TargetPath);
                var extension = Path.GetExtension(item.TargetPath);
                var path = fileNameWithoutExtension + "-" + hash + extension;

                if (directoryName != null)
                {
                    item.TargetPath = Path.Combine(directoryName, path);
                }
                else
                {
                    item.TargetPath = path;
                }
            }
        }
    }
}