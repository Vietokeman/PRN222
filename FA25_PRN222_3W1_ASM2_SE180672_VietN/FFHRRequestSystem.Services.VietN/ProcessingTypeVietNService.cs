using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFHRRequestSystem.Services.VietN
{
    public class ProcessingTypeVietNService
    {
        private readonly ProcessingTypeVietNRepository _repository;
        public ProcessingTypeVietNService() => _repository ??= new ProcessingTypeVietNRepository();
        public async Task<List<ProcessingTypeVietN>> GetAllAsync()
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
