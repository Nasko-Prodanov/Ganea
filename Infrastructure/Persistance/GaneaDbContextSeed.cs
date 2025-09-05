using System;
using System.Security.Cryptography.Pkcs;
using Infrastructure.Persistance.Entities;
using Infrastructure.Persistance.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Infrastructure.Persistance;

public static class GaneaDbContextSeed
{
    public async static Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        //Getting all the roles from the enum
        IEnumerable<string> roles = Enum.GetNames(typeof(Role));

        foreach (string r in roles)
        {
            bool isExistingRole = await roleManager.RoleExistsAsync(r);

            if (!isExistingRole)
            {
                //If result is unsuccessful, log it after integrating logging
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(r));
            }
        }
    }

    public static async Task SeedDevelopmentDataAsync(GaneaDbContext context)
    {
        bool hasData = await context.ProcedureCategories
            .AnyAsync();

        if (!hasData)
        {
            context.ProcedureCategories.AddRange
            (
                new ProcedureCategory { CategoryName = "Face Therapy" },
                new ProcedureCategory { CategoryName = "Wax" }
            );

            context.Procedures.AddRange(
                //Woman Wax
                new Procedure
                {
                    ProcedureName = "Upper lip",
                    Price = 10.00m,
                    Duration = 15,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Nose",
                    Price = 3.00m,
                    Duration = 15,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Chin",
                    Price = 10.00m,
                    Duration = 15,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Cheekbones",
                    Price = 10.00m,
                    Duration = 15,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Full Face",
                    Price = 25.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Armpits",
                    Price = 20.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Arms",
                    Price = 25.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Full Legs",
                    Price = 40.00m,
                    Duration = 60,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Thighs",
                    Price = 25.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Legs Up To The Knee",
                    Price = 20.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Bikini Area",
                    Price = 25.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Intimate",
                    Price = 35.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Abdomen",
                    Price = 15.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Lower Back",
                    Price = 15.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Buttocks",
                    Price = 15.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                //Man Wax
                new Procedure
                {
                    ProcedureName = "Cheekbones",
                    Price = 10.00m,
                    Duration = 15,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Ears",
                    Price = 10.00m,
                    Duration = 15,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Nose",
                    Price = 5.00m,
                    Duration = 15,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Armpits",
                    Price = 25.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Arms",
                    Price = 30.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Full Legs",
                    Price = 45.00m,
                    Duration = 60,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Shouders",
                    Price = 15.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Back & Lower Back",
                    Price = 30.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                new Procedure
                {
                    ProcedureName = "Chest & Abdomen",
                    Price = 35.00m,
                    Duration = 30,
                    ProcedureCategoryId = 2
                },
                //Facial Threatment
                new Procedure
                {
                    ProcedureName = "Fecial Cleansing",
                    Price = 70.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Ultrasonic Peeling",
                    Price = 55.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Ultrasonic Peeling with Sheet Mask",
                    Price = 60.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Sheet Mask",
                    Price = 40.00m,
                    Duration = 30,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Shaker Mask",
                    Price = 40.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Hydration Therapy",
                    Price = 85.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Anti-Acne Therapy",
                    Price = 85.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Lacto-Peel Therapy",
                    Price = 85.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Snail Mucin Therapy",
                    Price = 85.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Bioplasma Therapy",
                    Price = 85.00m,
                    Duration = 90,
                    ProcedureCategoryId = 1
                },
                //Eyebrow Treatments
                new Procedure
                {
                    ProcedureName = "Eyebrow Cleansing",
                    Price = 17.00m,
                    Duration = 30,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Eyebrow Tint",
                    Price = 10.00m,
                    Duration = 15,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Eyebrow Lamination",
                    Price = 50.00m,
                    Duration = 30,
                    ProcedureCategoryId = 1
                },
                new Procedure
                {
                    ProcedureName = "Eyebrow Therapy",
                    Price = 30.00m,
                    Duration = 30,
                    ProcedureCategoryId = 1
                }
            );

            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedDefaultAccount(
        IConfiguration configuration,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        bool emptyUserTable = !await userManager.Users
            .AnyAsync();

        bool notExistingAdmin = await userManager.Users
            .AnyAsync(u => u.Role.HasValue && u.Role == Role.Admin);

        if (emptyUserTable || notExistingAdmin)
        {
            string userName = configuration["DefaultUser:UserName"]!;
            string email = configuration["DefaultUser:Email"]!;
            string password = configuration["DefaultUser:Password"]!;

            User admin = new User
            {
                UserName = userName,
                Email = email
            };

            IdentityResult createUserResult = await userManager.CreateAsync(admin, password);

            if (!createUserResult.Succeeded)
            {
                //TODO: Log this
                throw new Exception($"Failed to create default user. Errors: {string.Join(", ", createUserResult.Errors)}");
            }

            IdentityResult addToRoleResult = await userManager.AddToRoleAsync(admin, Role.Admin.ToString());

            if (!addToRoleResult.Succeeded)
            {
                throw new InvalidOperationException($"Failed to add default user to role. Errors: {string.Join(", ", addToRoleResult.Errors)}");
            }
        }
    }

    //public static void SeedProductionData(GaneaDbContext context)
    //{

    //}
}
