using Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class GaneaDbContext : IdentityDbContext<User>
    {
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Procedure> Procedures { get; set; } = null!;
        public DbSet<ProcedureCategory> ProcedureCategories { get; set; } = null!;
        public DbSet<AppointmentProcedure> AppointmentProcedures { get; set; } = null!;

        public GaneaDbContext(DbContextOptions<GaneaDbContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Ganea;Integrated Security=True;TrustServerCertificate=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GaneaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
