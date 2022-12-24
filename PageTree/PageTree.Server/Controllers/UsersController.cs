using AutoMapper;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.UseCases.Users.Commands;
using System.Security.Claims;

namespace PageTree.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost,  Action("create")]
        public async Task<IActionResult> Create()
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest();

            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return BadRequest();

            var appCommand = new CreateUserCommand(idClaim.Value);
            return await _mediator.SendAndGetPostResponse(appCommand);
        }
    }
}
