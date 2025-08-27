using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistance.Entities
{
    public class User : IdentityUser
    {
        public Employee Employee { get; set; } = null!;
    }
}