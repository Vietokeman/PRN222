using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FFHRRequestSystem_RazorWebApp_VietN.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task HubCreate(string message)
        {
            await Clients.All.SendAsync("ReceiverCreate", message);
        }

        public async Task HubDelete(string id)
        {
            await Clients.All.SendAsync("ReceiverDelete", id);
        }
    }
}