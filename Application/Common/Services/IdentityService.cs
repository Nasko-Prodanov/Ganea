using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models.User;
using Application.Common.Validators;
using Infrastructure.Common.Models;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Entities;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly GaneaDbContext context;
        private readonly UserManager<User> userManager;
        private readonly JwtSettings settings;
        private readonly string jwtKey;

        public IdentityService(UserManager<User> userManager, GaneaDbContext context, IOptions<JwtSettings> options)
        {
            this.userManager = userManager;
            this.context = context;
            jwtKey = Environment.GetEnvironmentVariable("JWT_SECURITY_KEY")!;
            settings = options.Value;
        }

        public async Task CreateUserAsync(UserModel model, CancellationToken cancellationToken)
        {
            UserValidator.EmailDuplicateValidator(model.Email, context.Users.Any(u => u.Email == model.Email));
            UserValidator.UserNameValidator(model.UserName);
            UserValidator.UserEmailValidator(model.Email);
            UserValidator.EmailRegexValidator(model.Email);
            UserValidator.UserFirstNameValidator(model.FirstName);
            UserValidator.UserLastNameValidator(model.LastName);
            UserValidator.UserNameDuplicateValidator(model.UserName, context.Users.Any(u => u.UserName == model.UserName));
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

        public async Task<AuthResponse> AuthenticateAsync(LoginInputModel model, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                return new AuthResponse
                {
                    ErrorMessage = "Invalid User"
                };
            }

            bool passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return new AuthResponse
                {
                    ErrorMessage = "Invalid User"
                };
            }

            IEnumerable<Claim> claims = await userManager.GetClaimsAsync(user);

            string accessToken = GenerateAccessToken(user, claims, cancellationToken);
            string refreshToken = await GenerateRefreshTokenAsync(user, claims, cancellationToken);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateAccessToken(User user, IEnumerable<Claim> claims, CancellationToken token)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            string issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
            string audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;

            JwtSecurityToken tokenOptions = new(
                claims: claims,
                expires: DateTime.Now.AddMinutes(settings.AccessTokenExpiration),
                issuer: issuer,
                audience: audience,
                signingCredentials: credentials);

            string accessToken = new JwtSecurityTokenHandler()
                .WriteToken(tokenOptions);

            return accessToken;
        }

        private async Task<string> GenerateRefreshTokenAsync(User user, IEnumerable<Claim> claims, CancellationToken token)
        {
            string refreshToken = string.Empty;
            byte[] randomNumber = new byte[32];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }

            user.RefreshToken = refreshToken;
            user.RefreshTokenValidUntil = DateTime.Now
                .AddMinutes(settings.RefreshTokenExpiration);

            await userManager.UpdateAsync(user);

            return refreshToken;
        }
    }
}
