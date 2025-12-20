using Microsoft.AspNetCore.SignalR;

namespace LionPetManagement_NguyenViet
{
    public class NotificationHub : Hub
    {
        public async Task HubDelete(string id)
        {
            await Clients.All.SendAsync("ReceiverDelete", id);
        }
    }

}
