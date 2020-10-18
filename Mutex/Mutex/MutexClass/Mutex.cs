﻿using System;
using System.Threading;
using System.Collections.Generic;

namespace Mutex.MutexClass
{
    class Mutex : IMutex
    {
        #region Private Fields

        private int locker = 0;
        private int owner = -1;

        #endregion

        #region Contructor

        public Mutex() { }

        #endregion

        #region IMutex Implementation

        public void Lock()
        {
            while(Interlocked.CompareExchange(ref locker, 1, 0) != 0)
            {
                Thread.Sleep(20);
            }
            owner = Thread.CurrentThread.ManagedThreadId;
        }

        public void Unlock()
        {
            if (Thread.CurrentThread.ManagedThreadId != owner) throw new Exception("This thread doesn't own this mutex.");
            Interlocked.Exchange(ref locker, 0);            
        }

        #endregion
    }
}