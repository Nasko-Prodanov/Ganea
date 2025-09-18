using System.Security.Claims;
using System.Text;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Entities;
using Infrastructure.Settings;
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

            services.Configure<JwtSettings>(configuration.GetSection("Authentication:Jwt"));
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
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GaneaDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddTokenBasedAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.MapInboundClaims = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECURITY_KEY")!))
                    };
                });
        }
    }
}
