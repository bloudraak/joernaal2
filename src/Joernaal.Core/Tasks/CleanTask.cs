using System.IO;

namespace Joernaal.Tasks
{
    [Kind("Clean")]
    public class CleanTask : Task
    {
        public override void Execute(Context context)
        {
            var outputPath = context.Project.OutputPath;
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath);
            }
        }
    }
}