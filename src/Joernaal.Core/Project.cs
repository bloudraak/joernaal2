using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Joernaal.Tasks;
using Newtonsoft.Json;

namespace Joernaal
{
    /// <summary>
    ///     Represents a project
    /// </summary>
    public class Project : Collection
    {
        [JsonIgnore]
        public static TextWriter Trace { get; set; }

        static Project()
        {
            Trace = Console.Out;
        }


        [JsonProperty("Tasks")]
        public Dictionary<string, Task> Tasks { get; set; }

        public string SourcePath { get; set; }
        public string OutputPath { get; set; }

        public string TemplatePath { get; set; }

        public void Execute()
        {
            var context = new Context(this);
            Execute((task) => task.Init(context), "Init");
            Execute((task) => task.Execute(context), "Execute");
            Execute((task) => task.Done(context), "Done");
        }

        private void Execute(Action<Task> action, string methodName)
        {
            foreach (var t in Tasks)
            {
                Trace.WriteLine("Executing " + t.Key + "." + methodName + "...");
                action(t.Value);
                Trace.WriteLine("Executed " + t.Key + "." + methodName + "...");
            }
        }

        public static Project Create(string path)
        {
            var paths = new HashSet<string>();
            paths.Add(Path.Combine(path, ".metadata"));
            paths.Add(Path.Combine(path, "src", ".metadata"));
            paths.Add(Path.Combine(path, "source", ".metadata"));
            string projectPath = null;
            Project project = null;
            foreach (var metadataPath in paths)
            {
                if (!File.Exists(metadataPath))
                {
                    Trace.WriteLine("Project Metadata " + metadataPath + " doesn't exist...");
                    continue;
                }

                Trace.WriteLine("Project Metadata " + metadataPath + " exist...");
                using (var stream = File.OpenRead(metadataPath))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        projectPath = Path.GetDirectoryName(metadataPath);
                        project = Deserialize<Project>(reader);
                        break;
                    }
                }
            }

            if (project == null)
            {
                project = new Project();
            }

            if (string.IsNullOrWhiteSpace(project.SourcePath))
            {
                project.SourcePath = Path.Combine(path, "src");
            }

            if (string.IsNullOrWhiteSpace(project.OutputPath))
            {
                project.OutputPath = Path.Combine(path, "dist");
            }

            if (string.IsNullOrWhiteSpace(project.TemplatePath))
            {
                project.TemplatePath = Path.Combine(path, "templates");
            }

            // Complete Defaults
            return project;
        }
    }
}