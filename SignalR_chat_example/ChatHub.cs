using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalR_chat_example
{
    public class ChatHub : Hub
    {
        public void Broadcast(string message)
        {
            Clients.All.receive(Context.ConnectionId, message);
        }

        public void DeleteAll()
        {
            Clients.All.delete();
        }

        public override Task OnConnected()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            Clients.All.update(UserHandler.ConnectedIds.Count);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            Clients.All.update(UserHandler.ConnectedIds.Count);
            return base.OnDisconnected(stopCalled);
        }
    }
}