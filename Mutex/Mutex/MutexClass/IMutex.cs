using System;
using System.Collections.Generic;
using System.Text;

namespace Mutex.MutexClass
{
    interface IMutex
    {
        public void Lock();
        public void Unlock();
    }
}
