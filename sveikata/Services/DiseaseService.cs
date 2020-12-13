using Serilog;
using sveikata.DTOs;
using sveikata.Mappers;
using sveikata.Models;
using sveikata.Repositories.Interfaces;
using sveikata.Services.Interfaces;
using sveikata.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly AppDbContext _context;

        public DiseaseService(IDiseaseRepository diseaseRepository, AppDbContext context)
        {
            _diseaseRepository = diseaseRepository;
            _context = context;
        }

        public async Task<DiseaseResponse> Create(DiseaseDTO newDisease)
        {
            var disease = DiseaseMapper.Map(newDisease);
            if (disease == null)
            {
                string errorMessage = "Disease not found.";

                Log.Error(errorMessage);
                return new DiseaseResponse(errorMessage);
            }
            if (disease.Name == null)
            {
                string errorMessage2 = "Disease name not found.";
                Log.Error(errorMessage2);
                return new DiseaseResponse(errorMessage2);
            }
            if (disease.Description == null)
            {
                string errorMessage3 = "Disease description not found.";
                Log.Error(errorMessage3);
                return new DiseaseResponse(errorMessage3);
            }

            try
            {
                await _diseaseRepository.Create(disease);
                await _context.SaveChangesAsync();
                return new DiseaseResponse(DiseaseMapper.Map(disease));
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when creating the item: {exception.Message}";
                return new DiseaseResponse(errorMessage);
            }
        }

        

        public async Task<IEnumerable<DiseaseDTO>> GetAll()
        {
            return (await _diseaseRepository.GetAll()).Select(DiseaseMapper.Map).ToList();
        }

        public async Task<DiseaseResponse> GetById(int id)
        {
            var disease = await _diseaseRepository.GetById(id);

            if (disease == null)
            {
                string errorMessage = "Disease not found.";

                Log.Error(errorMessage);

                return new DiseaseResponse(errorMessage);
            }

            return new DiseaseResponse(DiseaseMapper.Map(disease));
        }

        public async Task<DiseaseResponse> Update(int id, DiseaseDTO updatedDisease)
        {
            var disease = await _diseaseRepository.GetById(id);
            if (disease == null)
            {
                throw new KeyNotFoundException();
            }

            //disease.Id = updatedDisease.Id;
            disease.Name = updatedDisease.Name;
            disease.Description = updatedDisease.Description;
            if (disease.Name == null)
            {
                string errorMessage3 = "Disease name not found.";
                Log.Error(errorMessage3);
                return new DiseaseResponse(errorMessage3);
            }
            if (disease.Description == null)
            {
                string errorMessage3 = "Disease description not found.";
                Log.Error(errorMessage3);
                return new DiseaseResponse(errorMessage3);
            }

            try
            {
                _diseaseRepository.Update(disease);
                await _context.SaveChangesAsync();
                return new DiseaseResponse();
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when updating the item: {exception.Message}";
                return new DiseaseResponse(errorMessage);
            }
        }

        public async Task<DiseaseResponse> Delete(int id)
        {
            var item = await _diseaseRepository.GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException();
            }

            try
            {
                _diseaseRepository.Delete(item);
                await _context.SaveChangesAsync();
                return new DiseaseResponse();
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when deleting the item: {exception.Message}";
                return new DiseaseResponse(errorMessage);
            }
        }
    }
}
