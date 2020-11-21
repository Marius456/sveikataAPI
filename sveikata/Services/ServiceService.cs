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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly AppDbContext _context;

        public ServiceService(IServiceRepository serviceRepository, AppDbContext context)
        {
            _serviceRepository = serviceRepository;
            _context = context;
        }

        public async Task<ServiceResponse> Create(ServiceDTO newService)
        {
            var service = ServiceMapper.Map(newService);

            if (service.Name == null)
            {
                string errorMessage1 = "Service name not found.";
                Log.Error(errorMessage1);
                return new ServiceResponse(errorMessage1);
            }
            if (service.Description == null)
            {
                string errorMessage2 = "Service description not found.";
                Log.Error(errorMessage2);
                return new ServiceResponse(errorMessage2);
            }
            try
            {
                await _serviceRepository.Create(service);
                await _context.SaveChangesAsync();
                return new ServiceResponse(ServiceMapper.Map(service));
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when creating the item: {exception.Message}";
                return new ServiceResponse(errorMessage);
            }
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var item = await _serviceRepository.GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException();
            }

            try
            {
                _serviceRepository.Delete(item);
                await _context.SaveChangesAsync();
                return new ServiceResponse();
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when deleting the item: {exception.Message}";
                return new ServiceResponse(errorMessage);
            }
        }

        public async Task<IEnumerable<ServiceDTO>> GetAll()
        {
            return (await _serviceRepository.GetAll()).Select(ServiceMapper.Map).ToList();
        }

        public async Task<ServiceResponse> GetById(int id)
        {
            var service = await _serviceRepository.GetById(id);

            if (service == null)
            {
                string errorMessage = "Service not found.";

                Log.Error(errorMessage);

                return new ServiceResponse(errorMessage);
            }

            return new ServiceResponse(ServiceMapper.Map(service));
        }

        public async Task<ServiceResponse> Update(int id, ServiceDTO updatedService)
        {
            var service = await _serviceRepository.GetById(id);
            if (service == null)
            {
                throw new KeyNotFoundException();
            }

            service.Id = updatedService.Id;
            service.Name = updatedService.Name;
            service.Description = updatedService.Description;

            if (service.Name == null)
            {
                string errorMessage1 = "Service name not found.";
                Log.Error(errorMessage1);
                return new ServiceResponse(errorMessage1);
            }
            if (service.Description == null)
            {
                string errorMessage2 = "Service description not found.";
                Log.Error(errorMessage2);
                return new ServiceResponse(errorMessage2);
            }
            try
            {
                _serviceRepository.Update(service);
                await _context.SaveChangesAsync();
                return new ServiceResponse();
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when updating the item: {exception.Message}";
                return new ServiceResponse(errorMessage);
            }
        }

       
    }
}
