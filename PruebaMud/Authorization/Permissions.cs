namespace PruebaMud.Authorization;

public static class Permissions
{
    public static class Users
    {
        public const string View = "Users.View";
        public const string Create = "Users.Create";
        public const string Edit = "Users.Edit";
        public const string Delete = "Users.Delete";
    }

    public static class Roles
    {
        public const string View = "Roles.View";
        public const string Create = "Roles.Create";
        public const string Edit = "Roles.Edit";
        public const string Delete = "Roles.Delete";
        public const string ManagePermissions = "Roles.ManagePermissions";
    }

    // public static class Patients
    // {
    //     public const string View = "Patients.View";
    //     public const string Create = "Patients.Create";
    //     public const string Edit = "Patients.Edit";
    //     public const string Delete = "Patients.Delete";
    // }

    // public static class Reports
    // {
    //     public const string View = "Reports.View";
    //     public const string Generate = "Reports.Generate";
    // }
}