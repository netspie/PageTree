using Corelibs.AspNetApi.Controllers;
using Corelibs.AspNetApi.Controllers.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.UseCases.Users.Commands;
using PageTree.App.UseCases.Users.Queries;
using System.Security.Claims;

namespace PageTree.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("me")]
        public Task<IActionResult> Get() =>
            _mediator.SendAndGetResponse(new GetUserQuery(UserID));

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest();

            var appCommand = new CreateUserCommand();
            return await _mediator.SendAndGetPostResponse(appCommand);
        }
    }
}
