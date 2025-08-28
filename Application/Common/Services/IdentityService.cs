using Application.Common.Interfaces;
using Application.Common.Validators;
using Infrastructure.Common.Models;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly GaneaDbContext context;
        private readonly UserManager<User> userManager;

        public IdentityService(UserManager<User> userManager, GaneaDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task CreateUserAsync(UserModel model, CancellationToken cancellationToken)
        {

            UserValidator.UserNameValidator(model.UserName);
            UserValidator.UserEmailValidator(model.Email);
            UserValidator.UserFirstNameValidator(model.FirstName);
            UserValidator.UserLastNameValidator(model.LastName);

            await context.SaveChangesAsync();

            string? password = Environment.GetEnvironmentVariable("DEFAULT_PASSWORD");

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "Password configuration missing");
            }

            //Creating the user
            User user = new()
            {
                UserName = model.UserName,
                Email = model.Email
            };

            IdentityResult result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));

                throw new InvalidOperationException(errorMessage);
            }

            Employee employee = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = user.Id
            };

            await context.Employees.AddAsync(employee);

            await context.SaveChangesAsync();
        }


    }
}
