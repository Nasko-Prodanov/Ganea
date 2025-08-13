using Infrastructure.Persistance.Enums;

namespace Infrastructure.Persistance.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public List<Appointment> Appointments { get; set; } = [];
    }
}
