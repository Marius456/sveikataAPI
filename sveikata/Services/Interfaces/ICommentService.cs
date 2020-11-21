using sveikata.DTOs;
using sveikata.Models;
using sveikata.Services.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sveikata.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAll();

        Task<CommentResponse> GetById(int id);
        Task<IEnumerable<CommentDTO>> GetByUser(int id);

        Task<CommentResponse> Create(CommentDTO item);

        Task<CommentResponse> Update(int id, CommentDTO item);

        Task<CommentResponse> Delete(int id);
    }
}
