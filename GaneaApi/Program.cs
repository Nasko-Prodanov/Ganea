using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GaneaApi;

public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var services = builder.Services;
        // Add services to the container.

        builder.Services.AddControllers();


        services.AddDbContext<GaneaDbContext>(opt =>
        opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            IServiceScope scope = services.BuildServiceProvider()
                .CreateScope()!;

            GaneaDbContext context = scope.ServiceProvider
                .GetRequiredService<GaneaDbContext>();

            await GaneaDbContextSeed.SeedDevelopmentDataAsync(context);

            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
