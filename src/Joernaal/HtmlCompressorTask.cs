using Joernaal.Html.Compressors;

namespace Joernaal
{
    public class HtmlCompressorTask : TextDocumentTask
    {
        protected override string Execute(string contents, TaskContext context)
        {
            var compressor = new HtmlCompressor();
            return compressor.compress(contents);
        }

        protected override bool ShouldExecute(Item item, TaskContext taskContext)
        {
            switch (item.Extension)
            {
                case ".html":
                case ".htm":
                    return true;

                default:
                    return false;
            }
        }
    }
}