using System;
using System.Runtime.InteropServices;

namespace Mutex.OSHandleClass
{
    class OSHandle
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        private bool _disposed = false;
        public IntPtr Handle { get; set; }

        public OSHandle()
        {
            Handle = IntPtr.Zero;
        }

        public OSHandle(IntPtr handle)
        {
            Handle = handle;
        }

        ~OSHandle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                if (!_disposed && Handle != IntPtr.Zero)
                {
                    CloseHandle(Handle);
                    Handle = IntPtr.Zero;
                }
                _disposed = true;
            }
        }
    }
}
