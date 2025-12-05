using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFHRRequestSystem.Services.VietN
{
    public class TicketProcessingVietNService
    {
        private readonly TicketProcessingVietNRepository _repository;
        public TicketProcessingVietNService() => _repository ??= new TicketProcessingVietNRepository();

        public async Task<List<TicketProcessingVietN>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TicketProcessingVietN> GetByIdAsync(Guid Id)
        {
            try
            {
                return await _repository.GetByIdAsync(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> CreateAsync(TicketProcessingVietN item)
        {
            try
            {
                return await _repository.CreateAsync(item);
            }
            catch (Exception ex)
            {
                // Log the inner exception for better debugging
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Error saving entity: {innerMessage}");
            }
        }

        public async Task<int> UpdateAsync(TicketProcessingVietN item)
        {
            try
            {
                return await _repository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item != null)
                {
                    return await _repository.RemoveAsync(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        public async Task<List<TicketProcessingVietN>> SearchAsync(string processingAction, string ActionDescription, string TypeName)
        {
            try
            {
                return await _repository.SearchAsync(processingAction, ActionDescription, TypeName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
