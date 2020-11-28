using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sveikata.Models;

namespace sveikata.ModelsConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(user => user.Comments).WithOne(comment => comment.Creator).HasForeignKey(comment => comment.UserId);
        }
    }
}
