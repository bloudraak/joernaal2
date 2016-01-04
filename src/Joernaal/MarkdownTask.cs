using System.Collections.Generic;
using System.IO;
using System.Text;
using MarkdownSharp;

namespace Joernaal
{
    public class MarkdownTask : TextDocumentTask
    {
        protected override void Execute(Item item, TaskContext context)
        {
            base.Execute(item, context);
            item.TargetPath = Path.ChangeExtension(item.TargetPath, ".html");
        }

        protected override string Execute(string contents, TaskContext context)
        {
            var markdown = new Markdown(false);
            contents = markdown.Transform(contents);
            return contents;
        }

        protected override bool ShouldExecute(Item item, TaskContext taskContext)
        {
            switch (item.Extension)
            {
                case ".md":
                case ".markdown":
                    return true;

                default:
                    return false;
            }
        }
    }
}