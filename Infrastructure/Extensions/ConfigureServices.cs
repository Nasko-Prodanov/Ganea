using System.Text;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extensions
{
    public static class ConfigureServices
    {
        public static void AddInfrstructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext();

            services.AddIdentity();
            services.AddTokenBasedAuthentication();
        }

        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<GaneaDbContext>(opt =>
                opt.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION")));
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GaneaDbContext>();
        }

        public static void AddTokenBasedAuthentication(this IServiceCollection services)
        {
            string? key = Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    };
                });
        }
    }
}
