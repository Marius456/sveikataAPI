using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sveikata.Models
{
    public class UserRoles
    {
        public string UserEmail { get; set; }
        public virtual User User { get; set; }

        public string RoleName { get; set; }
        public virtual Role Role { get; set; }
    }
}
