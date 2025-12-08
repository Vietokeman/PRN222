using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Threading.Tasks;

namespace FFHRRequestSystem_RazorWebApp_VietN.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task HubCreate(string jsonData)
        {
            // Deserialize the incoming JSON
            var data = JsonSerializer.Deserialize<JsonElement>(jsonData);
            
            // Re-serialize and broadcast to all clients
            await Clients.All.SendAsync("ReceiverCreate", jsonData);
        }

        public async Task HubUpdate(string jsonData)
        {
            // Deserialize the incoming JSON
            var data = JsonSerializer.Deserialize<JsonElement>(jsonData);
            
            // Re-serialize and broadcast to all clients
            await Clients.All.SendAsync("ReceiverUpdate", jsonData);
        }

        public async Task HubDelete(string ticketProcessingVietNid)
        {
            // Delete only needs the ID as a simple string
            await Clients.All.SendAsync("ReceiverDelete", ticketProcessingVietNid);
        }
    }
}