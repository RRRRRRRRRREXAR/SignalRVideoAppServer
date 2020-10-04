using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRVideoApp.Models;

namespace SignalRVideoApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NewMessage(Message message)
        {
            await Clients.All.SendAsync("messageReceived",message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
