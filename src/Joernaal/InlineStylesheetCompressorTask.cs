using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;

namespace Joernaal
{
    public class InlineStylesheetCompressorTask : TextDocumentTask
    {

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

        protected override string Execute(string contents, TaskContext context)
        {
            
            var doc = new HtmlDocument();
            doc.LoadHtml(contents);

            var nodesToDelete = new HashSet<HtmlNode>();

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//style"))
            {
                var innerText = node.InnerText;
                var compressor = new Microsoft.Ajax.Utilities.Minifier();
                var execute = compressor.MinifyStyleSheet(innerText);
                if (string.IsNullOrWhiteSpace(execute))
                {
                    nodesToDelete.Add(node);
                    continue;
                }

                node.ReplaceChild(HtmlNode.CreateNode(execute), node.FirstChild);

            }

            using (var writer = new StringWriter())
            {
                doc.Save(writer);
                return writer.ToString();
            }
        }
    }
}