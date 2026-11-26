using Microsoft.AspNetCore.Authorization;

namespace PruebaMud.Authorization;

public sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated != true)
        {
            return Task.CompletedTask;
        }

        var hasPermission = context.User.HasClaim(
            claim =>
                claim.Type == PermissionClaimTypes.Permission &&
                string.Equals(
                    claim.Value,
                    requirement.Permission,
                    StringComparison.OrdinalIgnoreCase));

        if (hasPermission)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}