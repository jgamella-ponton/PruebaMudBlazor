using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PruebaMud.Authorization;
using PruebaMud.Models.Security;

namespace PruebaMud.Services;

public class RoleAdministrationService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleAdministrationService(
        RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }


    public async Task<IdentityRole?> GetRoleAsync(string roleId)
    {
        return await _roleManager.FindByIdAsync(roleId);
    }


    public async Task<List<IdentityRole>> GetRolesAsync()
    {
        return await _roleManager.Roles
            .OrderBy(r => r.Name)
            .ToListAsync();
    }


    public async Task<List<PermissionGroup>>
        GetRolePermissionsAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role == null)
            return new();

        var claims = await _roleManager.GetClaimsAsync(role);

        var assignedPermissions = claims
            .Where(c => c.Type == "permission")
            .Select(c => c.Value)
            .ToHashSet();

        var groups = PermissionCatalog.GetAll();

        foreach (var group in groups)
        {
            foreach (var permission in group.Permissions)
            {
                permission.Selected =
                    assignedPermissions.Contains(permission.Name);
            }
        }

        return groups;
    }


    public async Task<IdentityResult> SavePermissionsAsync(
        string roleId,
        IEnumerable<string> permissions)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role == null)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Description = "Rol no encontrado."
                });
        }

        var currentClaims =
            await _roleManager.GetClaimsAsync(role);

        var permissionClaims = currentClaims
            .Where(c => c.Type == "permission")
            .ToList();


        // Eliminar permisos actuales

        foreach (var claim in permissionClaims)
        {
            var result =
                await _roleManager.RemoveClaimAsync(
                    role,
                    claim);

            if (!result.Succeeded)
                return result;
        }


        // Añadir los permisos seleccionados

        foreach (var permission in permissions.Distinct())
        {
            var result =
                await _roleManager.AddClaimAsync(
                    role,
                    new Claim(
                        "permission",
                        permission));

            if (!result.Succeeded)
                return result;
        }

        return IdentityResult.Success;
    }
}