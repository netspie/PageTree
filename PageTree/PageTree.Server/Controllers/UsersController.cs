using AutoMapper;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.UseCases.Users.Commands;
using PageTree.Server.ApiContracts.Pages;

namespace PageTree.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost, Route("{id}"), Action("create")]
        public async Task<IActionResult> Create()
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest();

            var claims = User.Claims.ToList();

            var appCommand = new CreateUserCommand("");
            return await _mediator.SendAndGetPostResponse(appCommand);
        }
    }
}
