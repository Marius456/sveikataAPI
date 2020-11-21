using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.DTOs
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
        [StringLength(200, ErrorMessage = "Password cannot exceed 200 characters.")]
        public string Password { get; set; }

        public bool isAdmin { get; set; }

        public ICollection<GetCommentsCollectionResponse> Comments { get; set; }
    }
}
