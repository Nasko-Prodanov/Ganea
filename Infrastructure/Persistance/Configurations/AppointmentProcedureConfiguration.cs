using Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class AppointmentProcedureConfiguration : IEntityTypeConfiguration<AppointmentProcedure>
    {
        public void Configure(EntityTypeBuilder<AppointmentProcedure> builder)
        {
            builder.HasKey(ap => new { ap.AppointmentId, ap.ProcedureId });

            builder.HasOne(ap => ap.Appointment)
                .WithMany(a => a.AppointmentProcedures)
                .HasForeignKey(ap => ap.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ap => ap.Procedure)
                .WithMany(p => p.AppointmentProcedures)
                .HasForeignKey(ap => ap.ProcedureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
