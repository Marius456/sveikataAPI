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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, AppDbContext context)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<CommentResponse> Create(CommentDTO newComment)
        {
            var comment = CommentMapper.Map(newComment);

            if (comment.UserId < 0)
            {
                string errorMessage = "Comment user ID not found.";
                Log.Error(errorMessage);
                return new CommentResponse(errorMessage);
            }
            if (comment.Text == null)
            {
                string errorMessage1 = "Comment context not found.";
                Log.Error(errorMessage1);
                return new CommentResponse(errorMessage1);
            }
            try
            {
                await _commentRepository.Create(comment);
                await _context.SaveChangesAsync();
                return new CommentResponse(CommentMapper.Map(comment));
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when creating the item: {exception.Message}";
                return new CommentResponse(errorMessage);
            }
        }


        public async Task<IEnumerable<CommentDTO>> GetAll()
        {
            return (await _commentRepository.GetAll()).Select(CommentMapper.Map).ToList();
        }

        public async Task<CommentResponse> GetById(int id)
        {
            var comment = await _commentRepository.GetById(id);

            if (comment == null)
            {
                string errorMessage = "Comment not found.";

                Log.Error(errorMessage);

                return new CommentResponse(errorMessage);
            }

            return new CommentResponse(CommentMapper.Map(comment));
        }

        public async Task<IEnumerable<CommentDTO>> GetByUser(int id)
        {
            return (await _commentRepository.GetByUser(id)).Select(CommentMapper.Map).ToList();
        }

        public async Task<CommentResponse> Update(int id, CommentDTO updatedComment, string userMail, bool isAdmin)
        {
            var comment = await _commentRepository.GetById(id);
            if (comment == null)
            {
                throw new KeyNotFoundException();
            }

            var user = _userRepository.FindByEmail(userMail);
            if (comment.UserId != user.Result.Id && !isAdmin)
            {
                string errorMessage = "You have no permition to edit this comment.";
                Log.Error(errorMessage);
                return new CommentResponse(errorMessage, false);
            }

            comment.Id = updatedComment.Id;
            comment.UserId = updatedComment.UserId;
            comment.Text = updatedComment.Text;

            if (comment.UserId < 0)
            {
                string errorMessage = "Comment user ID not found.";
                Log.Error(errorMessage);
                return new CommentResponse(errorMessage, true);
            }
            if (comment.Text == null)
            {
                string errorMessage1 = "Comment context not found.";
                Log.Error(errorMessage1);
                return new CommentResponse(errorMessage1, true);
            }

            try
            {
                _commentRepository.Update(comment);
                await _context.SaveChangesAsync();
                return new CommentResponse();
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when updating the item: {exception.Message}";
                return new CommentResponse(errorMessage, true);
            }
        }

        public async Task<CommentResponse> Delete(int id, string userMail, bool isAdmin)
        {
            var item = await _commentRepository.GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException();
            }

            var user = _userRepository.FindByEmail(userMail);
            if (item.UserId != user.Result.Id && !isAdmin)
            {
                string errorMessage = "You have no permition to edit this comment.";
                Log.Error(errorMessage);
                return new CommentResponse(errorMessage, false);
            }

            try
            {
                _commentRepository.Delete(item);
                await _context.SaveChangesAsync();
                return new CommentResponse();
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when deleting the item: {exception.Message}";
                return new CommentResponse(errorMessage, true);
            }
        }

    }
}
