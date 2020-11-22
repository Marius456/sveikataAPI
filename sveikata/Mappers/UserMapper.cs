using sveikata.DTOs;
using sveikata.Models;
using System.Collections.Generic;
using System.Linq;

namespace sveikata.Mappers
{
    public class UserMapper
    {
        public static UserDTO Map(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Comments = ToGetResponse(user.Comments)
            };
        }
        public static User Map(UserDTO itemDTO)
        {
            return new User()
            {
                Id = itemDTO.Id,
                Name = itemDTO.Name,
                Email = itemDTO.Email,
                Password = itemDTO.Password
            };
        }
        public static ICollection<GetCommentsCollectionResponse> ToGetResponse(ICollection<Comment> comments)
        {
            return comments?.Select(comment => new GetCommentsCollectionResponse
            {
                Id = comment.Id,
                Text = comment.Text,
                UserId = comment.UserId
            }).ToList();
        }
    }
}
