using sveikata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAll();

        Task<Comment> GetById(int id);
        Task<List<Comment>> GetByUser(int id);

        Task<Comment> Create(Comment item);

        void Update(Comment item);
        void Delete(Comment item);
    }
}
