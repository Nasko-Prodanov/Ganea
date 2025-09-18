using Application.Common.Models.User;
using Infrastructure.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task CreateUserAsync(UserModel model, CancellationToken cancellationToken);
        Task<AuthResponse> AuthenticateAsync(LoginInputModel model, CancellationToken cancellationToken);

        Task<IdentityResult> ChangeCurrentUserPasswordAsync(string userId,string oldPAssword, string newPassword);

        Task<IdentityResult> ResetPasswordAsync(string userId, string newPassword);
    }
}
