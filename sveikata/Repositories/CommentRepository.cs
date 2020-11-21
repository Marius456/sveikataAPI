using Microsoft.EntityFrameworkCore;
using sveikata.Models;
using sveikata.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> Create(Comment item)
        {
            await _context.Comments.AddAsync(item);
            return item;
        }

        public Task<List<Comment>> GetAll()
        {
            return _context.Comments.ToListAsync();
        }

        public Task<Comment> GetById(int id)
        {
            return _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task<List<Comment>> GetByUser(int id)
        {
            return _context.Comments.Where(item => item.UserId == id).ToListAsync();
        }

        public void Update(Comment item)
        {
            _context.Comments.Update(item);
        }

        public void Delete(Comment item)
        {
            _context.Comments.Remove(item);
        }

    }
}
