using PruebaMud.Models.Security;

namespace PruebaMud.Authorization;

public static class PermissionCatalog
{
    public static List<PermissionGroup> GetAll()
    {
        return new List<PermissionGroup>
        {
            // =========================================================
            // USUARIOS
            // =========================================================
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

            // =========================================================
            // ROLES
            // =========================================================
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

            // =========================================================
            // CLIENTES
            // =========================================================
            // new()
            // {
            //     Name = "Clientes",
            //     Permissions =
            //     {
            //         new()
            //         {
            //             Name = Permissions.Clientes.View,
            //             Description = "Consultar clientes"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Clientes.Create,
            //             Description = "Crear clientes"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Clientes.Edit,
            //             Description = "Modificar clientes"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Clientes.Delete,
            //             Description = "Eliminar clientes"
            //         }
            //     }
            // },

            // // =========================================================
            // // PEDIDOS
            // // =========================================================
            // new()
            // {
            //     Name = "Pedidos",
            //     Permissions =
            //     {
            //         new()
            //         {
            //             Name = Permissions.Pedidos.View,
            //             Description = "Consultar pedidos"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Pedidos.Create,
            //             Description = "Crear pedidos"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Pedidos.Edit,
            //             Description = "Modificar pedidos"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Pedidos.Delete,
            //             Description = "Eliminar pedidos"
            //         }
            //     }
            // },

            // =========================================================
            // REQUESTS
            // =========================================================
            // new()
            // {
            //     Name = "Solicitudes",
            //     Permissions =
            //     {
            //         new()
            //         {
            //             Name = Permissions.Requests.View,
            //             Description = "Consultar solicitudes"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Requests.Create,
            //             Description = "Crear solicitudes"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Requests.Edit,
            //             Description = "Modificar solicitudes"
            //         },
            //         new()
            //         {
            //             Name = Permissions.Requests.Delete,
            //             Description = "Eliminar solicitudes"
            //         }
            //     }
            // },

            // =========================================================
            // RESULTADOS
            // =========================================================
        //     new()
        //     {
        //         Name = "Resultados",
        //         Permissions =
        //         {
        //             new()
        //             {
        //                 Name = Permissions.Results.View,
        //                 Description = "Consultar resultados"
        //             },
        //             new()
        //             {
        //                 Name = Permissions.Results.Create,
        //                 Description = "Crear resultados"
        //             },
        //             new()
        //             {
        //                 Name = Permissions.Results.Edit,
        //                 Description = "Modificar resultados"
        //             },
        //             new()
        //             {
        //                 Name = Permissions.Results.Delete,
        //                 Description = "Eliminar resultados"
        //             }
        //         }
        //     }
         };
    }


    // =============================================================
    // DEVUELVE TODOS LOS PERMISOS EXISTENTES
    // =============================================================

    public static List<string> GetAllPermissionNames()
    {
        return GetAll()
            .SelectMany(group => group.Permissions)
            .Select(permission => permission.Name)
            .Distinct()
            .ToList();
    }
}