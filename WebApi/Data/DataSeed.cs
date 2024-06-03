using Microsoft.AspNetCore.Identity;
using WebApi.Entities;

namespace WebApi.Data
{
    public static class DataSeed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateAsyncScope();

            var roles = new string[] { "Admin", "Moderator" };

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                var existingRole = await roleManager.FindByNameAsync(role);
                if (existingRole != null) continue;
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            string adminUserName = (string)configuration["DefaultAdmin:UserName"]!;
            string adminPassword = (string)configuration["DefaultAdmin:Password"]!;

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var admin = await userManager.FindByNameAsync(adminUserName);

            if (admin is not null) { return; }

            admin = new AppUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = "Admin@gmail.com",
                UserName = adminUserName,
            };

            var result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                // Add the user to the "Admin" role
                await userManager.AddToRoleAsync(admin, roles[0]);
            }
        }
    }

}
