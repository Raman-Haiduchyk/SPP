using System;
using System.Runtime.InteropServices;

namespace Mutex.OSHandleClass
{
    public class OSHandle
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        private bool _disposed = false;
        public IntPtr Handle { get; set; }

        public OSHandle() : this(IntPtr.Zero)
        {
        }

        public OSHandle(IntPtr handle)
        {
            Handle = handle;
        }

        ~OSHandle()
        {
            CloseHandle();
        }

        public void Dispose()
        {
            CloseHandle();
            GC.SuppressFinalize(this);
        }

        private void CloseHandle()
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
