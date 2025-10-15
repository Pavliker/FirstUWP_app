using AlbumApp1._0._1.Interfaces;
using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services
{
    public partial class DispatcherQueueService:IDispatcherQueueService
    {
        private DispatcherQueue _dispatcherQueue = DispatcherQueue.GetForCurrentThread();

        public DispatcherQueue GetDispatcherQueue() => _dispatcherQueue;

        public void TryEnqueue(DispatcherQueuePriority priority, Action callback)
        {
            if (_dispatcherQueue != null) {

                _dispatcherQueue.TryEnqueue(priority, ()=>callback());
            }
        } 
    }
}
