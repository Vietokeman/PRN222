using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_Services
{
    public class LionProfileService
    {
        private readonly LionProfileRepository _repository;
        public LionProfileService()
        {
            _repository ??= new LionProfileRepository();
        }
        public async Task<List<LionProfile>> GetAllAsync()
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

        public async Task<LionProfile> GetByIdAsync(int Id)
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

        public async Task<int> CreateAsync(LionProfile item)
        {
            try
            {
                return await _repository.CreateAsync(item);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Error saving entity: {innerMessage}");
            }
        }

        public async Task<int> UpdateAsync(LionProfile item)
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

        public async Task<bool> DeleteAsync(int id)
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

        public async Task<List<LionProfile>> SearchAsync(double? weight, string lionTypeName)
        {
            try
            {
                return await _repository.SearchAsync(weight, lionTypeName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
