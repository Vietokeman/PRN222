using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Services.VietN;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FFHRRequestSystem_RazorWebApp_VietN.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly TicketProcessingVietNService _service;

        public NotificationHub(TicketProcessingVietNService service)
        {
            _service = service;
        }

        public async Task HubCreate(string jsonData)
        {
            var entity = JsonConvert.DeserializeObject<TicketProcessingVietN>(jsonData) ?? new TicketProcessingVietN();
            if (entity.TicketProcessingVietNid == Guid.Empty)
            {
                entity.TicketProcessingVietNid = Guid.NewGuid();
            }

            entity.CreatedDate = entity.CreatedDate == default ? DateTime.UtcNow : entity.CreatedDate;
            entity.ModifiedDate = DateTime.UtcNow;

            var result = await _service.CreateAsync(entity);
            if (result > 0)
            {
                var payload = JsonConvert.SerializeObject(entity);
                await Clients.All.SendAsync("ReceiverCreate", payload);
            }
        }

        public async Task HubUpdate(string jsonData)
        {
            var entity = JsonConvert.DeserializeObject<TicketProcessingVietN>(jsonData);
            if (entity == null || entity.TicketProcessingVietNid == Guid.Empty)
            {
                return;
            }

            entity.ModifiedDate = DateTime.UtcNow;
            var result = await _service.UpdateAsync(entity);
            if (result > 0)
            {
                var payload = JsonConvert.SerializeObject(entity);
                await Clients.All.SendAsync("ReceiverUpdate", payload);
            }
        }

        public async Task HubDelete(string ticketProcessingVietNid)
        {
            if (!Guid.TryParse(ticketProcessingVietNid, out var id))
            {
                return;
            }

            var result = await _service.DeleteAsync(id);
            if (result)
            {
                await Clients.All.SendAsync("ReceiverDelete", ticketProcessingVietNid);
            }
        }
    }
}