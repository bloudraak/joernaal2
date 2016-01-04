namespace Joernaal.Tasks
{
    public abstract class Task
    {
        public virtual void Init(Context context)
        {
        }

        public virtual void Execute(Context context)
        {
        }

        public virtual void Done(Context context)
        {
        }
    }
}