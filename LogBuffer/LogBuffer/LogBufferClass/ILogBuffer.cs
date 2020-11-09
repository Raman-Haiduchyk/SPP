using System;
using System.Collections.Generic;
using System.Text;

namespace LogBuffer.LogBufferClass
{
    interface ILogBuffer
    {
        void Add(string item);
        void ClearBufferAsync();

    }
}
