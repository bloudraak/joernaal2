namespace Joernaal
{
    public class CssCompressorTask : TextDocumentTask
    {
        protected override string Execute(string contents, TaskContext context)
        {
            var compressor = new Microsoft.Ajax.Utilities.Minifier();
            var execute = compressor.MinifyStyleSheet(contents);
            return execute;
        }

        protected override bool ShouldExecute(Item item, TaskContext taskContext)
        {
            switch (item.Extension)
            {
                case ".css":
                    return true;

                default:
                    return false;
            }
        }
    }
}