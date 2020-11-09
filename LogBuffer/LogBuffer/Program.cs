using System;
using System.Threading;
using LogBuffer.ParallelClass;
using LogBuffer.LogBufferClass;
using LogBuffer.CustomPoolClass;

namespace LogBuffer
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskDelegate[] tasks = new TaskDelegate[5];
            for (int i = 0; i < 5; i++)
            {
                int buf = i; 
                tasks[i] = delegate{ Console.WriteLine($"Task num: {buf}"); };
            }
            CustomParallel.WaitAll(tasks);
            LogBufferClass.LogBuffer logBuffer = new LogBufferClass.LogBuffer(messagesLimit: 5);
            int j = 0;
            while (true)
            {
                Thread.Sleep(5000);
                logBuffer.Add($"Message: {j}");
                if (j > 20000) break;
                j++;
            }
        }
    }
}
