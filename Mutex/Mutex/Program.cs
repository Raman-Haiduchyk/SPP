using System;
using System.Threading;

namespace Mutex
{
    class Program
    {
        static MutexClass.Mutex mutex = new MutexClass.Mutex();
        static void Main(string[] args)
        {
            Console.WriteLine("Started:");
            for (int i = 0; i < 5; i ++)
            {
                new Thread(new ThreadStart(Running)).Start();
            }
        //    Thread.Sleep(50);
        //    mutex.Unlock();
        }

        static public void Running()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} now is started.");
            mutex.Lock();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is working...");
                Thread.Sleep(400);
            }
            mutex.Unlock();
        }
    }
}
