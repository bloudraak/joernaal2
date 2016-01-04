using System.Collections.Generic;

namespace Joernaal
{
    public class TaskContext
    {
        public string TemplateDirectory { get; }

        public TaskContext(string sourceDirectory, string targetDirectory, string templateDirectory)
        {
            TemplateDirectory = templateDirectory;
            Items = new HashSet<Item>();
            SourceDirectory = sourceDirectory;
            TargetDirectory = targetDirectory;
        }

        public ISet<Item> Items { get; }

        public string SourceDirectory { get; }

        public string TargetDirectory { get; }
    }
}