namespace PruebaMud.Models.Security;

public class PermissionGroup
{
    public string Name { get; set; } = string.Empty;

    public List<PermissionItem> Permissions { get; set; } = new();
}

public class PermissionItem
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool Selected { get; set; }
}