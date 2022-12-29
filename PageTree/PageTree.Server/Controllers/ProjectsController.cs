using AutoMapper;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Projects.Commands;
using PageTree.Server.Authorization;
using PageTree.Server.Controllers;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProjectsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost, Action("create")]
        public Task<IActionResult> Create() =>
            _mediator.SendAndGetPostResponse(new CreateProjectCommand(UserID));

        [HttpPost, Action("changeName"), Authorize(Policy = AuthPolicies.Edit)]
        public Task<IActionResult> ChangeName()
        {
            Console.WriteLine(UserID);
            return Task.FromResult<IActionResult>(null);
        }
    }
}
