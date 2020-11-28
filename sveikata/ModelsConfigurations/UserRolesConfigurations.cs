using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sveikata.Models;

namespace sveikata.ModelsConfigurations
{
    public class UserRolesConfigurations : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.HasKey(x => new { x.UserEmail, x.RoleName });
        }
    }
}
