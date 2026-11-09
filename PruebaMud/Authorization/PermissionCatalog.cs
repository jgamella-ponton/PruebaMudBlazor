using PruebaMud.Models.Security;

namespace PruebaMud.Authorization;

public static class PermissionCatalog
{
    public static List<PermissionGroup> GetAll()
    {
        return new List<PermissionGroup>
        {
            new()
            {
                Name = "Usuarios",
                Permissions =
                {
                    new()
                    {
                        Name = Permissions.Users.View,
                        Description = "Consultar usuarios"
                    },
                    new()
                    {
                        Name = Permissions.Users.Create,
                        Description = "Crear usuarios"
                    },
                    new()
                    {
                        Name = Permissions.Users.Edit,
                        Description = "Modificar usuarios"
                    },
                    new()
                    {
                        Name = Permissions.Users.Delete,
                        Description = "Eliminar usuarios"
                    }
                }
            },

            new()
            {
                Name = "Roles",
                Permissions =
                {
                    new()
                    {
                        Name = Permissions.Roles.View,
                        Description = "Consultar roles"
                    },
                    new()
                    {
                        Name = Permissions.Roles.Create,
                        Description = "Crear roles"
                    },
                    new()
                    {
                        Name = Permissions.Roles.Edit,
                        Description = "Modificar roles"
                    },
                    new()
                    {
                        Name = Permissions.Roles.Delete,
                        Description = "Eliminar roles"
                    },
                    new()
                    {
                        Name = Permissions.Roles.ManagePermissions,
                        Description = "Gestionar permisos de roles"
                    }
                }
            },

            
        };
    }
}