using Microsoft.AspNet.SignalR;

namespace HugoHaggmark.Taskmanager.Hubs
{
    public class CpuHub : Hub
    {
        private readonly Broadcaster broadCaster;

        public CpuHub()
            : this(Broadcaster.Instance)
        {

        }

        public CpuHub(Broadcaster broadCaster)
        {
            this.broadCaster = broadCaster;
        }
    }
}