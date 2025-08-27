namespace Infrastructure.Persistance.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public List<Appointment> Appointments { get; set; } = [];

        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
