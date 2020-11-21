using Microsoft.EntityFrameworkCore;
using sveikata.DTOs.UserDTOs;
using sveikata.Models;
using sveikata.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User item)
        {
            await _context.Users.AddAsync(item);
            return item;
        }

        public Task<List<User>> GetAll()
        {
            return _context.Users.ToListAsync();
        }

        public Task<User> GetById(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(User item)
        {
            _context.Users.Update(item);
        }
        public void Delete(User item)
        {
            _context.Users.Remove(item);
        }

        public async Task<User> GetUser(LoginRequest request)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == request.Email && user.Password == request.Password);
        }
    }
}
