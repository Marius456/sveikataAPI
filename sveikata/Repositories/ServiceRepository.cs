using Microsoft.EntityFrameworkCore;
using sveikata.Models;
using sveikata.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Service> Create(Service item)
        {
            await _context.Services.AddAsync(item);
            return item;
        }

        public Task<List<Service>> GetAll()
        {
            return _context.Services.ToListAsync();
        }

        public Task<Service> GetById(int id)
        {
            return _context.Services.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Service item)
        {
            _context.Services.Update(item);
        }
        public void Delete(Service item)
        {
            _context.Services.Remove(item);
        }
    }
}
