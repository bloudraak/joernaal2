using System.Collections.Generic;

namespace Joernaal
{
    public interface ITask
    {
        void Execute(TaskContext taskContext);
    }
}