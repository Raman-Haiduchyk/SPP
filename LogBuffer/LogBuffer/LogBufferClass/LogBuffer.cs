using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogBuffer.LogBufferClass
{
    public class LogBuffer : ILogBuffer, IDisposable
    {
        #region Private Fields

        private const int defaultTimeLimit = 100000;
        private const int defaultMessagesLimit = 20;
        private const string defaultFilePath = @"log.txt";

        private List<string> _messagesList = new List<string>();
        private Timer _timer;
        private string _filePath;
        private int _messagesLimit;
        private object listSyncObject = new object();
        private object swSyncObject = new object();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates LogBufer exemplar.
        /// </summary>
        /// <param name="timeLimit"></param>
        /// <param name="messagesLimit"></param>
        /// <param name="filePath"></param>
        public LogBuffer(int timeLimit = defaultTimeLimit, int messagesLimit = defaultMessagesLimit, string filePath = defaultFilePath)
        {
            _filePath = filePath;
            _messagesLimit = messagesLimit;
            FileStream fs = null;
            if (timeLimit < 1 || messagesLimit < 1) throw new ArgumentException("Timer limit and Messages limit can't be less than 1");
            try
            {
                _timer = new Timer(new TimerCallback(TimerCount), null, timeLimit, timeLimit);
                fs = File.Create(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                fs?.DisposeAsync();
            }
        }

        #endregion

        private async void TimerCount(object state) 
        {
            await Task.Run(() => WriteToFile(true));
        }


        private void WriteToFile(bool isTimer)
        {
            List<string> buf = null;
            lock (listSyncObject)
            {
                if (!isTimer && _messagesList.Count < _messagesLimit) return;
                buf = new List<string>(_messagesList);
                _messagesList.Clear();
            }
            lock(swSyncObject)
            {
                using (StreamWriter sw = File.AppendText(_filePath))
                {
                    foreach (var msg in buf)
                    {
                        sw.WriteLine(msg);
                    }
                }
            }
            
        }

        #region ILogBuffer implementation

        public async void Add(string item)
        {
            bool overflowFlag = false;
            lock(listSyncObject)
            {
                _messagesList.Add($"[log-{DateTime.Now}]:"+item);
                overflowFlag = _messagesList.Count >= _messagesLimit;
            }
            if (overflowFlag) await Task.Run(() => WriteToFile(false));
        }

        public async void ClearBufferAsync()
        {
            await Task.Run(() => WriteToFile(true));
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            _timer.Dispose();
        }

        #endregion
    }
}
