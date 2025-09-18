using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models.ChangePassword
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
