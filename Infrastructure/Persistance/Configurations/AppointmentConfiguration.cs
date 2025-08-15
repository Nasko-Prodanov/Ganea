using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistance.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(apt => apt.AppointmentId);

            builder.HasOne(apt => apt.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(c => c.AppointmentId);
            
            builder.HasOne(apt => apt.Employee)
                .WithMany(c => c.Appointments)
                .HasForeignKey(c => c.EmployeeId);


        }
    }
}
