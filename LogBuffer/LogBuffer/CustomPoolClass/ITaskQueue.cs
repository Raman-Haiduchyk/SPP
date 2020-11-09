using System;
using System.Collections.Generic;
using System.Text;

namespace LogBuffer.CustomPoolClass
{

    delegate void TaskDelegate();

    interface ITaskQueue
    {
        void EnqueueTask(TaskDelegate taskDelegate);
    }
}
