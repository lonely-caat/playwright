using System.Threading;
using Zip.Api.CustomerProfile.Interfaces;

namespace Zip.Api.CustomerProfile.Services
{
    public class ServiceWarmupState : IServiceWarmupState
    {
        private int _taskCount;

        public void StartTask()
        {
            Interlocked.Increment(ref _taskCount);
        }

        public void MarkTaskAsComplete()
        {
            Interlocked.Decrement(ref _taskCount);
        }

        public bool IsReady => _taskCount == 0;
    }
}