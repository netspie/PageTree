using Corelibs.Basic.Architecture.DDD;
using Microsoft.AspNetCore.Authorization;
using PageTree.Server.Controllers;

namespace PageTree.Server.Authorization
{
    public class ResourceAuthorizationHandler<TResource> : AuthorizationHandler<SameAuthorRequirement, TResource>
        where TResource : IOwnedEntity
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SameAuthorRequirement requirement,
            TResource resource)
        {
            var userID = context.User.GetUserID();

            if (userID == resource.OwnerID)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class SameAuthorRequirement : IAuthorizationRequirement { }
}
