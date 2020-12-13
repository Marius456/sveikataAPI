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

        public CommentResponse() : base(string.Empty, true, true)
        {
        }

        public CommentResponse(string message) : base(message, false)
        {
        }
        public CommentResponse(string message, bool authorise) : base(message, false, authorise)
        {
        }
    }
}
