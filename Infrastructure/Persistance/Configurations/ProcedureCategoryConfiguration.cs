using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class ProcedureCategoryConfiguration : IEntityTypeConfiguration<ProcedureCategory>
    {
        public void Configure(EntityTypeBuilder<ProcedureCategory> builder)
        {
            builder.HasKey(pc => pc.ProcedureCategoryID);

            builder.Property(pc => pc.CategoryName)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
