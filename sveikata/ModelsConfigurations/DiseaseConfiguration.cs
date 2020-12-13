using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sveikata.Models;

namespace sveikata.ModelsConfigurations
{
    public class DiseaseConfiguration : IEntityTypeConfiguration<Disease>
    {
        public void Configure(EntityTypeBuilder<Disease> builder)
        {
            builder.HasData(new Disease
            {
                Id = 1,
                Name = "Pirmoji liga",
                Description = "Pirmosios ligos aprasymas"
            },
           new Disease
           {
               Id = 2,
               Name = "Antroji liga",
               Description = "Antrosios ligos aprasymas"
           },
           new Disease
           {
               Id = 3,
               Name = "Trecioji liga",
               Description = "Treciosios ligos aprasymas"
           });
        }
    }
}
