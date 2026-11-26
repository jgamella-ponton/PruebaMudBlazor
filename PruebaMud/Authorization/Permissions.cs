namespace PruebaMud.Authorization;

public static class Permissions
{
    public const string PolicyPrefix = "Permission:";

    public static class Users
    {
        public const string View = "Users.View";
        public const string Create = "Users.Create";
        public const string Edit = "Users.Edit";
        public const string Delete = "Users.Delete";

        public const string ViewPolicy = PolicyPrefix + View;
        public const string CreatePolicy = PolicyPrefix + Create;
        public const string EditPolicy = PolicyPrefix + Edit;
        public const string DeletePolicy = PolicyPrefix + Delete;
    }

    public static class Roles
    {
        public const string View = "Roles.View";
        public const string Create = "Roles.Create";
        public const string Edit = "Roles.Edit";
        public const string Delete = "Roles.Delete";
        public const string ManagePermissions =
            "Roles.ManagePermissions";

        public const string ViewPolicy = PolicyPrefix + View;
        public const string CreatePolicy = PolicyPrefix + Create;
        public const string EditPolicy = PolicyPrefix + Edit;
        public const string DeletePolicy = PolicyPrefix + Delete;
        public const string ManagePermissionsPolicy =
            PolicyPrefix + ManagePermissions;
    }

    // public static class Clientes
    // {
    //     public const string View = "Clientes.View";
    //     public const string Create = "Clientes.Create";
    //     public const string Edit = "Clientes.Edit";
    //     public const string Delete = "Clientes.Delete";

    //     public const string ViewPolicy = PolicyPrefix + View;
    //     public const string CreatePolicy = PolicyPrefix + Create;
    //     public const string EditPolicy = PolicyPrefix + Edit;
    //     public const string DeletePolicy = PolicyPrefix + Delete;
    // }

    // public static class Pedidos
    // {
    //     public const string View = "Pedidos.View";
    //     public const string Create = "Pedidos.Create";
    //     public const string Edit = "Pedidos.Edit";
    //     public const string Delete = "Pedidos.Delete";

    //     public const string ViewPolicy = PolicyPrefix + View;
    //     public const string CreatePolicy = PolicyPrefix + Create;
    //     public const string EditPolicy = PolicyPrefix + Edit;
    //     public const string DeletePolicy = PolicyPrefix + Delete;
    // }

    // public static class Requests
    // {
    //     public const string View = "Requests.View";
    //     public const string Create = "Requests.Create";
    //     public const string Edit = "Requests.Edit";
    //     public const string Delete = "Requests.Delete";

    //     public const string ViewPolicy = PolicyPrefix + View;
    //     public const string CreatePolicy = PolicyPrefix + Create;
    //     public const string EditPolicy = PolicyPrefix + Edit;
    //     public const string DeletePolicy = PolicyPrefix + Delete;
    // }

    // public static class Results
    // {
    //     public const string View = "Results.View";
    //     public const string Create = "Results.Create";
    //     public const string Edit = "Results.Edit";
    //     public const string Delete = "Results.Delete";

    //     public const string ViewPolicy = PolicyPrefix + View;
    //     public const string CreatePolicy = PolicyPrefix + Create;
    //     public const string EditPolicy = PolicyPrefix + Edit;
    //     public const string DeletePolicy = PolicyPrefix + Delete;
    // }
}