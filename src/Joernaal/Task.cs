namespace Joernaal
{
    public abstract class Task : ITask
    {
        public void Execute(TaskContext taskContext)
        {
            foreach (var item in taskContext.Items)
            {
                if (!ShouldExecute(item, taskContext))
                {
                    continue;
                }

                Execute(item, taskContext);
            }
        }

        protected abstract bool ShouldExecute(Item item, TaskContext taskContext);
        protected abstract void Execute(Item item, TaskContext context);
    }
}