using Application.Common.Interfaces;
using Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Extension
{
    public static class ConfigureServices
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IdentityService>();
            services.AddScoped<IProcedureCategoriesService, ProcedureCategoriesService>();
            services.AddScoped<IProcedureService, ProcedureService>();
        }
    }
}
