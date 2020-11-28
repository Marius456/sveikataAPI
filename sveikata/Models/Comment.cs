using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "User is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        public string Text { get; set; }

        public virtual User Creator { get; set; }
    }
}
