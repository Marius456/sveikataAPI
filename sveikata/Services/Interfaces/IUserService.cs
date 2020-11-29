using sveikata.DTOs;
using sveikata.DTOs.User;
using sveikata.Models;
using sveikata.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sveikata.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();

        Task<UserResponse> GetById(int id);

        Task<IEnumerable<CommentDTO>> GetUserCommentsList(int id);

        Task<IEnumerable<CommentDTO>> GetByComment(int id, int commentId);

        Task<UserResponse> Create(UserDTO item, params ERole[] userRoles);

        Task<UserResponse> Update(int id, UserDTO item);

        Task<UserResponse> Delete(int id);



        Task<LoginResponse<AuthenticatedUserDTO>> LoginUser(PostUserDTO UserCredentials);
        Task<User> FindByEmailAsync(string email);
    }
}
