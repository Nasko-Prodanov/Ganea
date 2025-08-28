using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistance.Entities
{
    public class User : IdentityUser
    {
        public Employee Employee { get; set; } = null!;

        public DateTime? RefreshTokenValidUntil { get; set; }

        public string? RefreshToken { get; set; }
    }
}