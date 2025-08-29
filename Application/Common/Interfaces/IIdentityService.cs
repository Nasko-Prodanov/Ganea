using Application.Common.Models.User;
using Infrastructure.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task CreateUserAsync(UserModel model, CancellationToken cancellationToken);
        Task<AuthResponse> AuthenticateAsync(LoginInputModel model, CancellationToken cancellationToken);
    }
}
