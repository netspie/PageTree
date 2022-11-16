using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Functional;
using Common.Basic.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PageTree.Domain;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;
using System.Security.Claims;

namespace PageTree.Api.Authorization
{
    public class AllowedForPageEditHandler : CustomAuthorizationHandler<AllowedForPageEditRequirement>
    {
        private readonly IRepository<Page> _pageRepository;

        protected override async Task<Result> Validate(AuthorizationHandlerContext context, AllowedForPageEditRequirement requirement)
        {
            var result = Result.Success();

            var userID = context.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userID == default || userID.Value.IsNullOrEmpty())
                return context.FailResult(result);

            var page = await _pageRepository.Get(userID.Value, result);
            if (!result.ValidateSuccessAndValues())
                return context.FailResult(result);

            if (page.AuthorUserID != userID.Value)
                return context.FailResult(result);

            return context.SucceedResult(requirement, result);
        }
    }

    public static class exts
    {
        public static Result FailResult(this AuthorizationHandlerContext context, Result result)
        {
            context.Fail();
            result.Fail();

            return result;
        }

        public static Result SucceedResult(this AuthorizationHandlerContext context, IAuthorizationRequirement requirement, Result result)
        {
            context.Succeed(requirement);
            return result;
        }
    }

    public abstract class CustomAuthorizationHandler<TRequirement> : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var result = await Validate(context, requirement);
            ///log
        }

        protected abstract Task<Result> Validate(AuthorizationHandlerContext context, TRequirement requirement);
    }
}
