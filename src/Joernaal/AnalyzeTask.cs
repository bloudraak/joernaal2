using System.IO;

namespace Joernaal
{
    public class AnalyzeTask : ITask
    {
        public void Execute(TaskContext context)
        {
            var files = Directory.GetFiles(context.SourceDirectory, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                context.Items.Add(new Item(file, context.SourceDirectory));
            }
        }
    }
}