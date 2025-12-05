using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FFHRRequestSystem_RazorWebApp_VietN.Hubs
{
    public class TicketProcessingHub : Hub
    {
        public async Task HubCreate_TicketProcessingVietN(string processingCode)
        {
            await Clients.All.SendAsync("Receiver_CreateTicketProcessingVietN", processingCode);
        }

        public async Task HubDelete_TicketProcessingVietN(Guid ticketProcessingId)
        {
            await Clients.All.SendAsync("Receiver_DeleteTicketProcessingVietN", ticketProcessingId.ToString());
        }
    }
}