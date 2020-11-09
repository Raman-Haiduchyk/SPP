using LogBuffer.CustomPoolClass;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogBuffer.ParallelClass
{
    static class CustomParallel
    {
        private static TaskQueue _taskQueue = new TaskQueue();

        public static void WaitAll(TaskDelegate[] tasks)
        {
            foreach (var task in tasks)
            {
                _taskQueue.EnqueueTask(task);             
            }
            _taskQueue.StopWorking();
        }
    }
}
