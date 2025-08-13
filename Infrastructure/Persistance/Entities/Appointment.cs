using System.ComponentModel.Design;

namespace Infrastructure.Persistance.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public DateTime PlanedDate { get; set; }
        public bool IsActive { get; set; }

        public List<AppointmentProcedure> AppointmentProcedures { get; set; } = [];
    }
}
