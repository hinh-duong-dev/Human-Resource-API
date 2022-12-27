using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
            (
                new Company
                { 
                    Id = Guid.NewGuid(),
                    Name = "Microsoft",
                    Address = "Redmond, Washington, U.S.",
                    Country = "USA",
                },
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple",
                    Address = "312 Forest Avenue, California",
                    Country = "USA",
                }
            );
        }
    }
}
