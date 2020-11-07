using System;
using System.Threading;
using System.Collections.Generic;

namespace Mutex.MutexClass
{
    public class Mutex : IMutex
    {
        #region Private Fields

        private int owner = -1;

        #endregion

        #region Contructor

        public Mutex() { }

        #endregion

        #region IMutex Implementation

        public void Lock()
        {
            while(Interlocked.CompareExchange(ref owner, Thread.CurrentThread.ManagedThreadId, -1) != -1)
            {
                Thread.Sleep(20);
            }
        }

        public void Unlock()
        {
            if (Thread.CurrentThread.ManagedThreadId != owner) throw new Exception("This thread doesn't own this mutex.");
            Interlocked.Exchange(ref owner, -1);            
        }

        #endregion
    }
}
