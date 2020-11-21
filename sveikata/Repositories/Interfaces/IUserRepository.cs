using sveikata.DTOs.UserDTOs;
using sveikata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> Create(User item);

        void Update(User item);
        void Delete(User item);
        Task<User> GetUser(LoginRequest request);
    }
}
