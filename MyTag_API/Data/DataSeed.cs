﻿using Microsoft.AspNetCore.Identity;
using MyTag_API.Entities;

namespace MyTag_API.Data
{
    public class DataSeed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();

            var roles = new string[] { "Admin", "User" };

            var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                var existingRole = await rolemanager.FindByNameAsync(role);

                if (existingRole is not null) continue;

                await rolemanager.CreateAsync(new IdentityRole(role));
            }

            string adminUserName = (string)configuration["DefaultAdmin:UserName"]!;
            string adminPassword = (string)configuration["DefaultAdmin:Password"]!;

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var rector = await userManager.FindByNameAsync(adminUserName);

            if (rector is not null) { return; }

            rector = new AppUser
            {
                Name = "Nuraib",
                Email = adminUserName,
                UserName = adminUserName,
            };

            var token = await userManager.GenerateEmailConfirmationTokenAsync(rector);

            await userManager.ConfirmEmailAsync(rector, token);

            var result = await userManager.CreateAsync(rector, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(rector, roles[0]);
            }
        }
    }
}