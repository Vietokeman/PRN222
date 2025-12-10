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
            Console.WriteLine($"🔔 [HubCreate] Received: {jsonData}");
            
            var entity = JsonConvert.DeserializeObject<TicketProcessingVietN>(jsonData) ?? new TicketProcessingVietN();
            if (entity.TicketProcessingVietNid == Guid.Empty)
            {
                entity.TicketProcessingVietNid = Guid.NewGuid();
            }

            entity.CreatedDate = entity.CreatedDate == default ? DateTime.UtcNow : entity.CreatedDate;
            entity.ModifiedDate = DateTime.UtcNow;

            Console.WriteLine($"📝 [HubCreate] Creating entity with ID: {entity.TicketProcessingVietNid}");
            
            var result = await _service.CreateAsync(entity);
            Console.WriteLine($"✅ [HubCreate] Create result: {result}");
            
            if (result > 0)
            {
                // Reload entity with navigation properties to get TypeName
                var savedEntity = await _service.GetByIdAsync(entity.TicketProcessingVietNid);
                Console.WriteLine($"🔄 [HubCreate] Reloaded entity: {savedEntity?.ProcessingCode}");
                
                var payload = JsonConvert.SerializeObject(savedEntity, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                
                Console.WriteLine($"📤 [HubCreate] Broadcasting to all clients: {payload}");
                await Clients.All.SendAsync("ReceiverCreate", payload);
                Console.WriteLine($"✨ [HubCreate] Broadcast complete!");
            }
            else
            {
                Console.WriteLine($"❌ [HubCreate] Failed to create entity");
            }
        }

        public async Task HubUpdate(string jsonData)
        {
            Console.WriteLine($"🔔 [HubUpdate] Received: {jsonData}");
            
            var entity = JsonConvert.DeserializeObject<TicketProcessingVietN>(jsonData);
            if (entity == null || entity.TicketProcessingVietNid == Guid.Empty)
            {
                Console.WriteLine($"❌ [HubUpdate] Invalid entity or empty ID");
                return;
            }

            Console.WriteLine($"📝 [HubUpdate] Updating entity with ID: {entity.TicketProcessingVietNid}");
            
            entity.ModifiedDate = DateTime.UtcNow;
            var result = await _service.UpdateAsync(entity);
            Console.WriteLine($"✅ [HubUpdate] Update result: {result}");
            
            if (result > 0)
            {
                // Reload entity with navigation properties to get TypeName
                var updatedEntity = await _service.GetByIdAsync(entity.TicketProcessingVietNid);
                Console.WriteLine($"🔄 [HubUpdate] Reloaded entity: {updatedEntity?.ProcessingCode}");
                
                var payload = JsonConvert.SerializeObject(updatedEntity, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                
                Console.WriteLine($"📤 [HubUpdate] Broadcasting to all clients: {payload}");
                await Clients.All.SendAsync("ReceiverUpdate", payload);
                Console.WriteLine($"✨ [HubUpdate] Broadcast complete!");
            }
            else
            {
                Console.WriteLine($"❌ [HubUpdate] Failed to update entity");
            }
        }

        public async Task HubDelete(string ticketProcessingVietNid)
        {
            Console.WriteLine($"🔔 [HubDelete] Received ID: {ticketProcessingVietNid}");
            
            if (!Guid.TryParse(ticketProcessingVietNid, out var id))
            {
                Console.WriteLine($"❌ [HubDelete] Invalid GUID format");
                return;
            }

            Console.WriteLine($"🗑️ [HubDelete] Deleting entity with ID: {id}");
            
            var result = await _service.DeleteAsync(id);
            Console.WriteLine($"✅ [HubDelete] Delete result: {result}");
            
            if (result)
            {
                Console.WriteLine($"📤 [HubDelete] Broadcasting to all clients: {ticketProcessingVietNid}");
                await Clients.All.SendAsync("ReceiverDelete", ticketProcessingVietNid);
                Console.WriteLine($"✨ [HubDelete] Broadcast complete!");
            }
            else
            {
                Console.WriteLine($"❌ [HubDelete] Failed to delete entity");
            }
        }
    }
}