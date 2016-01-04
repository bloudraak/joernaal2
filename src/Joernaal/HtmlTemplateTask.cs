using System;
using System.IO;
using System.Security;
using System.Security.Policy;
using System.Web.Razor;
using RazorEngine;
using RazorEngine.Templating;

namespace Joernaal
{
    public class HtmlTemplateTask : TextDocumentTask
    {

        protected override string Execute(string contents, TaskContext context)
        {
            var templateDirectory = context.TemplateDirectory;
            if (!Directory.Exists(templateDirectory))
            {
                return contents;
            }


            using (var service = RazorEngineService.Create())
            {
                foreach (var file in Directory.GetFiles(templateDirectory, "*.cshtml"))
                {
                    string name = Path.GetFileNameWithoutExtension(file)?.ToLower();
                    if (name == null)
                    {
                        name = Path.GetFileName(file);
                    }
                    var source = File.ReadAllText(file);
                    service.AddTemplate(name, source);
                }

                var s = Guid.NewGuid().ToString("N");
                service.AddTemplate(s, @"@{Layout = ""default"";}" + contents);
                service.Compile(s);
                return service.Run(s);
            }
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

        public static AppDomain CreateSandbox()
        {
            Evidence ev = new Evidence();
            ev.AddHostEvidence(new Zone(SecurityZone.Internet));
            PermissionSet permissionSet = SecurityManager.GetStandardSandbox(ev);
            StrongName razorEngineAssembly = typeof(RazorEngineService).Assembly.Evidence.GetHostEvidence<StrongName>();
            StrongName razorAssembly = typeof(RazorTemplateEngine).Assembly.Evidence.GetHostEvidence<StrongName>();
            AppDomainSetup adSetup = new AppDomainSetup();
            adSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permissionSet, razorEngineAssembly, razorAssembly);
            return newDomain;
        }

    }
}