using Application.Common.Extension;
using Application.Common.Services;
using Infrastructure.Extensions;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Entities;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace GaneaApi;

public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var services = builder.Services;
        // Add services to the container.
        builder.Services.AddIdentityCore<IdentityUser>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
        })
         .AddRoles<IdentityRole>()                              // много важно за RoleManager
         .AddEntityFrameworkStores<GaneaDbContext>()            // свързва към БД
         .AddSignInManager();                                   //builder.Services.AddScoped<IdentityService>();

        builder.Services.AddOptions<JwtSettings>();
        builder.Services.AddInfrstructure(configuration);
        builder.Services.AddApplication();
        builder.Services.AddControllers();
        builder.Services.AddIdentity();
        builder.Services.AddOpenApiDocument(configure =>
         {
             configure.Title = "Ganea API";
             //Not supported in .NET 8
             //configure.GenerateEnumMappingDescription = true;

             configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
             {
                 Scheme = JwtBearerDefaults.AuthenticationScheme,
                 Type = OpenApiSecuritySchemeType.ApiKey,
                 In = OpenApiSecurityApiKeyLocation.Header,
                 BearerFormat = "JWT",
                 Name = "Authorization",
                 Description = "Type into the textbox: Bearer {your JWT token}."
             });

             configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
         })
            .Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true); // Customize default API behavior

        var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddScoped<RoleService>();
        var app = builder.Build();

        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GaneaDbContext>();
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await context.Database.MigrateAsync();

            await GaneaDbContextSeed.SeedRolesAsync(roleManager);
            await GaneaDbContextSeed.SeedDefaultAccount(configuration, userManager, roleManager);

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

        app.UseOpenApi();

        //app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ganea API V1");
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();
        app.MapControllers();

        app.MapGet("/", () => Results.Redirect("/swagger"));

        app.Run();
    }
}
