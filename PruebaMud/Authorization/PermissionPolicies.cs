namespace PruebaMud.Authorization;

public static class PermissionPolicies
{
    public static string For(string permission)
    {
        return $"{PermissionPolicyProvider.PolicyPrefix}{permission}";
    }
}