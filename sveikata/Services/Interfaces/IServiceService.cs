using sveikata.DTOs;
using sveikata.Models;
using sveikata.Services.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sveikata.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDTO>> GetAll();

        Task<ServiceResponse> GetById(int id);

        Task<ServiceResponse> Create(ServiceDTO item);

        Task<ServiceResponse> Update(int id, ServiceDTO item);

        Task<ServiceResponse> Delete(int id);
    }
}
