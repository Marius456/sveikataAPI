using sveikata.DTOs;

namespace sveikata.Services.Responses
{
    public class UserResponse : BaseResponse
    {
        public UserDTO user { get; set; }

        public UserResponse(UserDTO item) : base(string.Empty, true)
        {
            this.user = item;
        }

        public UserResponse() : base(string.Empty, true)
        {
        }

        public UserResponse(string message) : base(message, false)
        {
        }
    }
}
