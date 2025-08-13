using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.HasKey(p => p.ProcedureId);

            builder.Property(p => p.ProcedureName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.HasOne(p => p.ProcedureCategory)
                .WithMany(pc => pc.Procedures)
                .HasForeignKey(pc => pc.ProcedureCategoryId);
        }
    }
}
