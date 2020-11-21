using sveikata.DTOs;
using sveikata.Models;
using sveikata.Services.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sveikata.Services.Interfaces
{
    public interface IDiseaseService
    {
        Task<IEnumerable<DiseaseDTO>> GetAll();

        Task<DiseaseResponse> GetById(int id);

        Task<DiseaseResponse> Create(DiseaseDTO disease);

        Task<DiseaseResponse> Update(int id, DiseaseDTO disease);

        Task<DiseaseResponse> Delete(int id);
    }
}
