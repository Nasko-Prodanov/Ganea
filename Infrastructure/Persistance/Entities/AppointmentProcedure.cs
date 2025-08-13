using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Entities
{
    public class AppointmentProcedure
    {
        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; } = null!;

        public int ProcedureId { get; set; }

        public Procedure Procedure { get; set; } = null!;
    }
}
