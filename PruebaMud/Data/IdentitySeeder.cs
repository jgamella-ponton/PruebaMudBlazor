using Microsoft.AspNetCore.Identity;

namespace PruebaMud.Data;

public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        // Obtener los servicios de Identity
        var roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var userManager =
            serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


        // 1. Crear roles si no existen
        string[] roles =
        {
            "Administrator",
            "Technician",
            "Viewer"
        };

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(roleName));
            }
        }


        // 2. Buscar el usuario que será administrador
        var adminEmail = "jgamella@hotmail.com";

        var adminUser =
            await userManager.FindByEmailAsync(adminEmail);


        // 3. Si existe, asignarle el rol Administrator
        if (adminUser != null)
        {
            var isAdmin =
                await userManager.IsInRoleAsync(
                    adminUser,
                    "Administrator");

            if (!isAdmin)
            {
                await userManager.AddToRoleAsync(
                    adminUser,
                    "Administrator");
            }
        }
    }
}