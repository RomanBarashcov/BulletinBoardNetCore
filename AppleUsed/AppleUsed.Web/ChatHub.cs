using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web
{
    public class Chat : Hub
    {
        public async Task Send(string message, string senderId)
        {
            await Clients.All.SendAsync("Send", message, senderId);
        }
    }
}
