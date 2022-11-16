using Microsoft.AspNetCore.Authorization;

namespace PageTree.Api.Authorization
{
    public class AllowedForPageEditRequirement : IAuthorizationRequirement
    {
        public AllowedForPageEditRequirement()
        {

        }
    }
}
