using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.Models
{
    public class Role
    {

        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<UserRoles> UsersRole { get; set; } = new Collection<UserRoles>();
    }

    public enum ERole
    {
        Common,
        Administrator
    }
}
