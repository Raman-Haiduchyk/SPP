using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CustomThreadPool.CustomPool;

namespace CustomThreadPool.CustomPool
{
    class TaskQueue : ITaskQueue
    {
        #region Private fields

        private const int _defaultThreadCount = 5;

        private bool _isWorking = true;

        private object _locker = new object();

        private int _threadsCount;

        private Thread[] _threads;

        private Queue<TaskDelegate> _taskQueue = new Queue<TaskDelegate>();

        #endregion

        #region Constructor

        public TaskQueue() : this(_defaultThreadCount) { }

        public TaskQueue(int count)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException("count", "Количество одновременно выполняемых задач должно быть больше 0.");
            }
            _threadsCount = count;
            _threads = new Thread[_threadsCount];
            for(int i = 0; i < _threadsCount; i++)
            {
                _threads[i] = new Thread(new ThreadStart(ThreadLoop));
                _threads[i].Start();
            }
        }

        #endregion

        #region Private Methods

        private void ThreadLoop()
        {
            while (_isWorking)
            {
                TaskDelegate task = null;
                lock (_locker)
                {
                    if (_taskQueue.Count == 0)
                    {
                        Monitor.Wait(_locker);
                    }
                    else
                    {
                        task = _taskQueue.Dequeue();
                        Monitor.Pulse(_locker);
                    }
                }
                if (task != null) task();
            }
        }

        #endregion

        #region Public Methods

        public void StopWorking()
        {
            while(true)
            {
                Thread.Sleep(300);
                lock(_locker)
                {
                    if (_taskQueue.Count == 0) break;
                }
            }
            lock (_locker)
            {
                _isWorking = false;
                Monitor.PulseAll(_locker);
            }
        }

        #endregion

        #region ITaskQueue Implementation

        public void EnqueueTask(TaskDelegate taskDelegate)
        {
            lock (_locker)
            {
                _taskQueue.Enqueue(taskDelegate);
                Monitor.Pulse(_locker);
            }
        }

        #endregion
    }
}
