using Application.Common.Interfaces;
using Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class RoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task EnsureRolesAsync(IEnumerable<string> roles, CancellationToken cancellationToken)
        {
            foreach (var role in roles)
            {
                if (!await RoleExistsAsync(role, cancellationToken))
                {
                    await CreateRoleAsync(role, cancellationToken);
                }
            }
        }

        public async Task<bool> RoleExistsAsync(string roleName, CancellationToken cancellationToken)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var role = new IdentityRole(roleName);
            return await roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }
            return await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IReadOnlyList<IdentityResult>> GetAllSync(CancellationToken cancellationToken)
        {
            var roles = await roleManager.Roles.ToListAsync(cancellationToken);
            var results = new List<IdentityResult>();
            foreach (var role in roles)
            {
                results.Add(IdentityResult.Success);
            }
            return results;
        }
    }
}
