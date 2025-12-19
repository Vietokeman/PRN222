using LionPetManagement.Entities.Models;
using LionPetManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement.Services
{
    public class LionProfileService
    {
        private readonly LionProfileRepository _repo;
        public LionProfileService()
        {
            _repo ??= new LionProfileRepository();
        }
        
        public async Task<LionProfile> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<LionProfile>> GetAllAsync()
        {
            return await _repo.GetAll();
        }

        public async Task<List<LionProfile>> SearchAsync(double? weight, string typeName)
        {
            return await _repo.SearchAsync(weight, typeName);
        }

        public async Task<int> CreateAsync(LionProfile item)
        {
            try
            {
                return await _repo.CreateAsync(item);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Error saving entity: {innerMessage}");
            }

        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var item = await _repo.GetByIdAsync(id);
                if (item != null)
                {
                    return await _repo.RemoveAsync(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public async Task<int> UpdateAsync(LionProfile item)
        {
            try
            {
                return await _repo.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
