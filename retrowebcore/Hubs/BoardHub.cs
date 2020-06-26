using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Hubs
{
    public class BoardHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Thread.Sleep(4000);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
