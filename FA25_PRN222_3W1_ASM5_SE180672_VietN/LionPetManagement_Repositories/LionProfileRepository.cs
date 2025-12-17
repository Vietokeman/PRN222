using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.Base;
using LionPetManagement_Repositories.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_Repositories
{
    public class LionProfileRepository : GenericRepository<LionProfile>
    {
        public LionProfileRepository()
        {
            
        }
        public LionProfileRepository(SU25LionDBContext context)
        {
            _context = context;
        }

        public async Task<List<LionProfile>> GetAll()
        {
            try
            {
                return await _context.LionProfiles.Include(c => c.LionType).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LionProfile> GetByIdAsync(int id)
        {
            try
            {
                return await _context.LionProfiles.Include(a => a.LionType).FirstOrDefaultAsync(l => l.LionProfileId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LionProfile>> SearchAsync(double? weight, string lionTypeName)
        {
            try
            {
                var query = _context.LionProfiles.Include(c => c.LionType).AsQueryable();

                // OR logic: Filter by Weight OR LionTypeName
                if (weight.HasValue || !string.IsNullOrEmpty(lionTypeName))
                {
                    query = query.Where(c =>
                        (weight.HasValue && c.Weight == weight.Value) ||
                        (!string.IsNullOrEmpty(lionTypeName) && c.LionType.LionTypeName.Contains(lionTypeName))
                    );
                }

                return await query
                    .OrderByDescending(c => c.LionProfileId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
