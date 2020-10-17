using System;
using System.Collections.Generic;
using System.Text;

namespace CustomThreadPool.CustomPool
{

    delegate void TaskDelegate();

    interface ITaskQueue
    {
        void EnqueueTask(TaskDelegate taskDelegate);
    }
}
