using Application.Common.Extension;
using Infrastructure.Extensions;
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

        builder.Services.AddInfrstructure(configuration);
        builder.Services.AddApplication();
        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GaneaDbContext>();

            await context.Database.MigrateAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                await GaneaDbContextSeed.SeedDevelopmentDataAsync(context);

                app.MapOpenApi();
            }
        }
        catch (Exception)
        {
            //TODO: Log the exception
            throw;
        }


        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ganea API V1");
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
