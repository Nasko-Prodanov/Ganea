using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models
{
    public class UserModel
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = null!;
    }
}
