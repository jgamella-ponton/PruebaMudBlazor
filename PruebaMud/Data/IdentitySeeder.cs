using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PruebaMud.Authorization;

namespace PruebaMud.Data;

public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(
        IServiceProvider serviceProvider)
    {
        var roleManager =
            serviceProvider.GetRequiredService<
                RoleManager<IdentityRole>>();

        var userManager =
            serviceProvider.GetRequiredService<
                UserManager<ApplicationUser>>();


        // =========================================================
        // 1. CREAR ROLES SI NO EXISTEN
        // =========================================================

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
                var result = await roleManager.CreateAsync(
                    new IdentityRole(roleName));

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"No se pudo crear el rol '{roleName}'. " +
                        string.Join(
                            ", ",
                            result.Errors.Select(e => e.Description)));
                }
            }
        }


        // =========================================================
        // 2. ASIGNAR TODOS LOS PERMISOS A ADMINISTRATOR
        // =========================================================

        var administratorRole =
            await roleManager.FindByNameAsync("Administrator");

        if (administratorRole == null)
        {
            throw new InvalidOperationException(
                "No se ha encontrado el rol Administrator.");
        }


        // Obtener los claims que ya tiene Administrator
        var existingClaims =
            await roleManager.GetClaimsAsync(administratorRole);


        // Quedarnos solamente con los claims de permisos
        var existingPermissions = existingClaims
            .Where(c =>
                c.Type == PermissionClaimTypes.Permission)
            .Select(c => c.Value)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);


        // Obtener todos los permisos definidos en nuestro catálogo
        var allPermissions =
            PermissionCatalog.GetAllPermissionNames();


        // Añadir solamente los permisos que falten
        foreach (var permission in allPermissions)
        {
            if (!existingPermissions.Contains(permission))
            {
                var result =
                    await roleManager.AddClaimAsync(
                        administratorRole,
                        new Claim(
                            PermissionClaimTypes.Permission,
                            permission));

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"No se pudo asignar el permiso " +
                        $"'{permission}' al rol Administrator. " +
                        string.Join(
                            ", ",
                            result.Errors.Select(
                                e => e.Description)));
                }
            }
        }


        // =========================================================
        // 3. ASIGNAR EL USUARIO ADMINISTRADOR INICIAL
        // =========================================================

        var adminEmail = "jgamella@hotmail.com";

        var adminUser =
            await userManager.FindByEmailAsync(adminEmail);


        if (adminUser != null)
        {
            var isAdmin =
                await userManager.IsInRoleAsync(
                    adminUser,
                    "Administrator");

            if (!isAdmin)
            {
                var result =
                    await userManager.AddToRoleAsync(
                        adminUser,
                        "Administrator");

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"No se pudo asignar el rol Administrator " +
                        $"al usuario '{adminEmail}'. " +
                        string.Join(
                            ", ",
                            result.Errors.Select(
                                e => e.Description)));
                }
            }
        }
    }
}