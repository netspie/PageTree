using Microsoft.AspNetCore.Authorization;

namespace PageTree.Server.Authorization
{
    public class AllowedForPageEditRequirement : IAuthorizationRequirement
    {
        public AllowedForPageEditRequirement()
        {

        }
    }
}
