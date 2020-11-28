using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sveikata.Models;
using System;

namespace sveikata.ModelsConfigurations
{
    public class RolesConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            var roleEnums = Enum.GetValues(typeof(ERole));
            foreach(var roleEnum in roleEnums)
            {
                builder.HasData(new Role { Name = roleEnum.ToString() });
            }
        }
    }
}
