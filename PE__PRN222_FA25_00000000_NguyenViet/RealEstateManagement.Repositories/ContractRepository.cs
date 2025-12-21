using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstateManagement.Entities.Models;
using RealEstateManagement.Repositories.Base;
using RealEstateManagement.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagement.Repositories
{
    public class ContractRepository : GenericRepository<Contract>
    {
        public ContractRepository()
        {
            
        }
        public ContractRepository(FA25RealEstateDBContext context)
        {
            _context = context;
        }

        public async Task<List<Contract>> GetAll()
        {
            try
            {
                return await _context.Contracts.Include(c => c.Broker).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Contract> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Contracts.Include(a => a.Broker).FirstOrDefaultAsync(l => l.ContractId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Contract>> SearchAsync(string conTractTitle, string propertyType, DateOnly signningDate)
        {
            var query = _context.Contracts.Include(c => c.Broker).AsQueryable();

            if (!string.IsNullOrEmpty(conTractTitle) || !string.IsNullOrEmpty(propertyType) || !string.IsNullOrEmpty(signningDate.ToString()))
            {
                query = query.Where(c =>
                    (!string.IsNullOrEmpty(conTractTitle) && c.ContractTitle.Contains(conTractTitle)) ||
                    !string.IsNullOrEmpty(propertyType) &&
                    (c.
                    PropertyType.Contains(propertyType))

                    || (!string.IsNullOrEmpty(signningDate.ToString()) && c.SigningDate.Day == signningDate.Day));
                    ;
            }

            return await query
                .OrderByDescending(c => c.ContractId)
                .ToListAsync();
        }
    }
}
