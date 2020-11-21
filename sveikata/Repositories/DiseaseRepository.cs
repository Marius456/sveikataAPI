using Microsoft.EntityFrameworkCore;
using sveikata.Models;
using sveikata.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Repositories
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly AppDbContext _context;

        public DiseaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Disease> Create(Disease disease)
        {
            await _context.Diseases.AddAsync(disease);
            return disease;
        }

        public Task<List<Disease>> GetAll()
        {
            return _context.Diseases.ToListAsync();
        }

        public Task<Disease> GetById(int id)
        {
            return _context.Diseases.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Disease disease)
        {
            _context.Diseases.Update(disease);
        }

        public void Delete(Disease disease)
        {
            _context.Diseases.Remove(disease);
        }
    }
}
