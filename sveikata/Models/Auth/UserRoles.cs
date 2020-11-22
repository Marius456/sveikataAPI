using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
