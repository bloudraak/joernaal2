namespace Joernaal
{
    public class JavaScriptCompressorTask : TextDocumentTask
    {
        protected override string Execute(string contents, TaskContext context)
        {
            var compressor = new Microsoft.Ajax.Utilities.Minifier();
            var execute = compressor.MinifyJavaScript(contents);
            return execute;
        }

        protected override bool ShouldExecute(Item item, TaskContext taskContext)
        {
            switch (item.Extension)
            {
                case ".js":
                    return true;

                default:
                    return false;
            }
        }
    }
}