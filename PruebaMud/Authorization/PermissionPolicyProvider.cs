using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace PruebaMud.Authorization;

public sealed class PermissionPolicyProvider
    : DefaultAuthorizationPolicyProvider
{
    public const string PolicyPrefix = "Permission:";

    public PermissionPolicyProvider(
        IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(
        string policyName)
    {
        if (!policyName.StartsWith(
                PolicyPrefix,
                StringComparison.OrdinalIgnoreCase))
        {
            return await base.GetPolicyAsync(policyName);
        }

        var permission = policyName[PolicyPrefix.Length..].Trim();

        if (string.IsNullOrWhiteSpace(permission))
        {
            return null;
        }

        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddRequirements(
                new PermissionRequirement(permission))
            .Build();
    }
}