using sveikata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Repositories.Interfaces
{
    public interface IDiseaseRepository
    {
        Task<List<Disease>> GetAll();

        Task<Disease> GetById(int id);

        Task<Disease> Create(Disease disease);

        void Update(Disease disease);
        void Delete(Disease disease);
    }
}
