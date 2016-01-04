namespace Joernaal
{
    public class Context
    {
        public Project Project { get; set; }

        public Context(Project project)
        {
            Project = project;
        }
    }
}