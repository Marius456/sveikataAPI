using sveikata.DTOs;

namespace sveikata.Services.Responses
{
    public class ServiceResponse : BaseResponse
    {
        public ServiceDTO service { get; set; }

        public ServiceResponse(ServiceDTO item) : base(string.Empty, true)
        {
            this.service = item;
        }

        public ServiceResponse() : base(string.Empty, true)
        {
        }

        public ServiceResponse(string message) : base(message, false)
        {
        }
    }
}
