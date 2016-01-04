using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace Joernaal
{
    public class FixHtmlReferenceTask : TextDocumentTask
    {
        private Dictionary<string, string> _mapping;

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

        protected override void Execute(Item item, TaskContext context)
        {
            _mapping = CreateMapping(context, item.TargetPath);

            base.Execute(item, context);
        }

        protected override string Execute(string contents, TaskContext context)
        {
            
            var doc = new HtmlDocument();
            doc.LoadHtml(contents);

            doc.ReplaceReferences("link", "href", _mapping);
            doc.ReplaceReferences("a", "href", _mapping);
            doc.ReplaceReferences("script", "src", _mapping);

            using (var writer = new StringWriter())
            {
                doc.Save(writer);
                return writer.ToString();
            }
        }


        private static Dictionary<string, string> CreateMapping(TaskContext context, string path)
        {
            path = path.Replace("\\", "/");
            UriBuilder builder = new UriBuilder("http", "localhost", 80, path);
            var uri = builder.Uri;
            
             
            var mapping = new Dictionary<string, string>();
            foreach (var item in context.Items)
            {
                var targetPath = item.TargetPath.Replace("\\", "/");
                if (targetPath == path)
                {
                    continue;
                }
                builder = new UriBuilder("http", "localhost", 80, targetPath);
                var targetUri = builder.Uri;

                var makeRelativeUri = uri.MakeRelativeUri(targetUri);
                var value = makeRelativeUri.ToString();
                // what do we do if its empty? 
                mapping.Add(item.Key, value);
                if (item.Key.Contains("\\"))
                {
                    mapping.Add(item.Key.Replace("\\", "/"), value);
                }
            }
            return mapping;
        }
    }
}