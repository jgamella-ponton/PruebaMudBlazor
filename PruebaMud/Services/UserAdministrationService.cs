using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PruebaMud.Data;

namespace PruebaMud.Services;
using PruebaMud.Models.Security;
public class UserAdministrationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserAdministrationService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<UserListItem>> GetUsersAsync()
    {
        var users = await _userManager.Users
            .OrderBy(x => x.Email)
            .ToListAsync();

        var result = new List<UserListItem>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            result.Add(new UserListItem
            {
                Id = user.Id,
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                Roles = roles.ToList(),
                IsLocked = user.LockoutEnd.HasValue &&
                           user.LockoutEnd > DateTimeOffset.UtcNow
            });
        }

        return result;
    }

    public async Task<ApplicationUser?> GetUserAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<List<string>> GetRolesAsync()
    {
        return await _roleManager.Roles
            .Select(x => x.Name!)
            .OrderBy(x => x)
            .ToListAsync();
    }

    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return new List<string>();

        return await _userManager.GetRolesAsync(user);
    }

    public async Task<IdentityResult> AddToRoleAsync(
        string userId,
        string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Description = "Usuario no encontrado."
                });
        }

        return await _userManager.AddToRoleAsync(user, roleName);
    }

  public async Task<IdentityResult> RemoveFromRoleAsync(
    string userId,
    string roleName)
{
    var user = await _userManager.FindByIdAsync(userId);

    if (user is null)
    {
        return IdentityResult.Failed(
            new IdentityError
            {
                Description = "Usuario no encontrado."
            });
    }

    // Protección especial para el rol Administrator
    if (roleName == "Administrator")
    {
        var administrators =
            await _userManager.GetUsersInRoleAsync(
                "Administrator");

        var activeAdministrators = administrators
            .Where(x =>
                !x.LockoutEnd.HasValue ||
                x.LockoutEnd <= DateTimeOffset.UtcNow)
            .ToList();

        if (activeAdministrators.Count == 1 &&
            activeAdministrators[0].Id == user.Id)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                Code = "LastAdministrator",
                Description =
                    "No se puede eliminar el rol Administrator del último administrador activo del sistema."
            });
        }
    }

    return await _userManager.RemoveFromRoleAsync(
        user,
        roleName);
}

   public async Task<IdentityResult> LockUserAsync(string userId)
{
    var user = await _userManager.FindByIdAsync(userId);

    if (user is null)
    {
        return IdentityResult.Failed(
            new IdentityError
            {
                Description = "Usuario no encontrado."
            });
    }

    // Comprobar si el usuario es Administrator
    var isAdministrator =
        await _userManager.IsInRoleAsync(
            user,
            "Administrator");

    if (isAdministrator)
    {
        var administrators =
            await _userManager.GetUsersInRoleAsync(
                "Administrator");

        // Contamos únicamente administradores no bloqueados
        var activeAdministrators = administrators
            .Where(x =>
                !x.LockoutEnd.HasValue ||
                x.LockoutEnd <= DateTimeOffset.UtcNow)
            .ToList();

        if (activeAdministrators.Count == 1 &&
            activeAdministrators[0].Id == user.Id)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "LastAdministrator",
                    Description =
                        "No se puede bloquear al último administrador activo del sistema."
                });
        }
    }

    await _userManager.SetLockoutEnabledAsync(
        user,
        true);

    return await _userManager.SetLockoutEndDateAsync(
        user,
        DateTimeOffset.MaxValue);
}

    public async Task<IdentityResult> UnlockUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Description = "Usuario no encontrado."
                });
        }

        return await _userManager.SetLockoutEndDateAsync(user, null);
    }
}