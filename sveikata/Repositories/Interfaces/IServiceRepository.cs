using sveikata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAll();

        Task<Service> GetById(int id);

        Task<Service> Create(Service item);

        void Update(Service item);
        void Delete(Service item);
    }
}
