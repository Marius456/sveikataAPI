using sveikata.DTOs.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.DTOs
{
    public class ErrorDTO
    {
        public List<Error> Errors { get; set; }

        public ErrorDTO()
        {
            Errors = new List<Error>();
        }
    }
}
