using LionPetManagement.Entities.Models;
using LionPetManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement.Services
{
    public class LionTypeService 
    {
        private readonly LionTypeRepository _repository;
        public LionTypeService()
        {
            _repository ??= new LionTypeRepository();
        }
        public async Task<List<LionType>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
