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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(ei => ei.Id);
            
            builder.Property(ei => ei.FirstName)
                .HasMaxLength(20)
                .IsRequired();
            
            builder.Property(ei => ei.LastName)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
