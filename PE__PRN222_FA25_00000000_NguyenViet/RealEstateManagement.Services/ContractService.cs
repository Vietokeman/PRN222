using RealEstateManagement.Entities.Models;
using RealEstateManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagement.Services
{
    public class ContractService
    {
        private readonly ContractRepository _repo;
        public ContractService()
        {
            _repo = new ContractRepository();
        }

        public async Task<List<Contract>> GetAllAsync()
        {

            return await _repo.GetAll();
        }

        public async Task<Contract> GetByIdAsync(int Id)
        {
            return await _repo.GetByIdAsync(Id);

        }
        public async Task<int> CreateAsync(Contract item)
        {
            return await _repo.CreateAsync(item);
        }

        public async Task<int> UpdateAsync(Contract item)
        {
            return await _repo.UpdateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var item = await _repo.GetByIdAsync(id);
            if (item != null)
            {
                return await _repo.RemoveAsync(item);
            }
            return false;
        }
    }
}
