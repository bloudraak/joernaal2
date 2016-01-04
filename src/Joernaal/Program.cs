using System;
using System.Collections.Generic;
using System.IO;

namespace Joernaal
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            var sourcePath = @"S:\dummy\src";
            var destinationPath = Path.GetFullPath(Path.Combine(sourcePath, "..", "dist"));
            var templateDirectory = Path.Combine(sourcePath, "..", "templates");

            var context = new TaskContext(sourcePath, destinationPath, templateDirectory);

            ITask[] tasks = {
                new CleanTask(),
                new AnalyzeTask(),
                new MarkdownTask(),
                new HtmlTemplateTask(),
                new InlineJavaScriptCompressorTask(),
                new InlineStylesheetCompressorTask(), 
                //new HtmlCompressorTask(),
                //new CssCompressorTask(),
                //new JavaScriptCompressorTask(),
                new HashifyTask(),
                new FixHtmlReferenceTask(), 
            };

            foreach (var task in tasks)
            {
                task.Execute(context);
            }

            foreach (var item in context.Items)
            {
                var targetPath = Path.Combine(destinationPath, item.TargetPath);
                var targetDirectory = Path.GetDirectoryName(targetPath);
                if (targetDirectory != null) Directory.CreateDirectory(targetDirectory);
                File.WriteAllBytes(targetPath, item.Contents);
            }

            foreach (var item in context.Items)
            {
                Console.WriteLine(item.Key + " " + item.SourcePath + " -> " + item.TargetPath);
            }
        }
    }
}