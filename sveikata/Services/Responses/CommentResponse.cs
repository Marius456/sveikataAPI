using sveikata.DTOs;

namespace sveikata.Services.Responses
{
    public class CommentResponse : BaseResponse
    {
        public CommentDTO comment { get; set; }

        public CommentResponse(CommentDTO item) : base(string.Empty, true)
        {
            this.comment = item;
        }

        public CommentResponse() : base(string.Empty, true)
        {
        }

        public CommentResponse(string message) : base(message, false)
        {
        }
    }
}
