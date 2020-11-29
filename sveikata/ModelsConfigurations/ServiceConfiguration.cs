using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sveikata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sveikata.ModelsConfigurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(new Service
            {
                Id = 1,
                Name = "Pirmasis gydymo budas",
                Description = "Pirmojo gydymo budo aprasymas"
            },
           new Service
           {
               Id = 2,
               Name = "Antrasis gydymo budas",
               Description = "Antrojo gydymo budo aprasymas"
           },
           new Service
           {
               Id = 3,
               Name = "Treciasis gydymo budas",
               Description = "Treciojo gydymo budo aprasymas"
           });
        }
    }
}
