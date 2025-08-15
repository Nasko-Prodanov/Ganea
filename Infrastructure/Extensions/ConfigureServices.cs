using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ConfigureServices
    {
        public static void AddInfrstructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GaneaDbContext>(opt =>
                opt.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION")));
        }
    }
}
