using System;
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
           
        }
    }
}
