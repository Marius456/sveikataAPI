using sveikata.DTOs;
using sveikata.DTOs.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Services.Responses
{
    public abstract class BaseResponse
    {
        public ErrorDTO Messages { get; set; }
        public bool Success { get; set; }

        public BaseResponse(string message, bool success)
        {
            Messages = new ErrorDTO();
            Messages.Errors.Add(new Error() { Message = message });
            Success = success;
        }
    }
}
