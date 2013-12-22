using Microsoft.AspNet.SignalR;
using System;

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

        public void InitRootUri()
        {
            this.broadCaster.Root = new Uri(Context.Request.Url.GetLeftPart(UriPartial.Authority));
        }
    }
}
