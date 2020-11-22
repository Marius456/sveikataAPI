using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.DTOs.User
{
    public class PostUserDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(32)]
        public string Password { get; set; }
    }
}
