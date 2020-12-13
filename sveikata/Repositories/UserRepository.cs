using Microsoft.EntityFrameworkCore;
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

        public async Task<User> Create(User user, ERole[] userRoles)
        {
            //await _context.Users.AddAsync(item);

            var roleNames = userRoles.Select(r => r.ToString()).ToList();
            var roles = await _context.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRoles { RoleName = role.Name, UserEmail = user.Email });
            }

            _context.Users.Add(user);
            return user;
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

        public async Task<User> FindByEmail(string email)
        {
            return await _context.Users.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
