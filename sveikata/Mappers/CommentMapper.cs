using sveikata.DTOs;
using sveikata.Models;

namespace sveikata.Mappers
{
    public class CommentMapper
    {
        public static CommentDTO Map(Comment item)
        {
            return new CommentDTO()
            {
                Id = item.Id,
                UserId = item.UserId,
                Text = item.Text
            };
        }
        public static Comment Map(CommentDTO itemDTO)
        {
            return new Comment()
            {
                Id = itemDTO.Id,
                UserId = itemDTO.UserId,
                Text = itemDTO.Text
            };
        }
    }
}
