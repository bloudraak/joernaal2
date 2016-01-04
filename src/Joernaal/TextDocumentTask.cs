using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Joernaal
{
    public abstract class TextDocumentTask : Task
    {
        protected override void Execute(Item item, TaskContext context)
        {
            var originalContents = item.ContentsAsText();
            var modifiedContents = Execute(originalContents, context);
            if (originalContents != modifiedContents)
            {
                item.SetContents(modifiedContents);
            }
        }

        protected abstract string Execute(string contents, TaskContext context);
    }
}