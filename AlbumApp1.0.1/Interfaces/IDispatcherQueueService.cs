using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IDispatcherQueueService
    {
        DispatcherQueue GetDispatcherQueue();
        void TryEnqueue(DispatcherQueuePriority normal, Action value);
    }

}
