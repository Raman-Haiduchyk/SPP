using System;
using System.Threading;
using Mutex.OSHandleClass;
using System.Runtime.InteropServices;

namespace Mutex
{
    class Program
    {

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        const int STD_OUTPUT_HANDLE = -11;

        static MutexClass.Mutex mutex = new MutexClass.Mutex();
        static void Main(string[] args)
        {
            Console.WriteLine("Started:");
            for (int i = 0; i < 5; i ++)
            {
                new Thread(new ThreadStart(Running)).Start();
            }

            Console.ReadLine();
            OSHandle handle = new OSHandle(GetStdHandle(STD_OUTPUT_HANDLE));
            handle.Dispose();
            try
            {
                Console.WriteLine("shi");
            }
            catch (Exception ex)
            {
                
            }
            
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
