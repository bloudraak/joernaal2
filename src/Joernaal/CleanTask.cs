using System.IO;

namespace Joernaal
{
    public class CleanTask : ITask
    {
        public void Execute(TaskContext context)
        {
            //
            // Clean Distribute Directory
            //
            if (Directory.Exists(context.TargetDirectory))
            {
                Directory.Delete(context.TargetDirectory, true);
            }
        }
    }
}