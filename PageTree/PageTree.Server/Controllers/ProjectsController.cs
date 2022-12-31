using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Projects.Commands;
using PageTree.App.Projects.Queries;
using PageTree.Server.ApiContracts.Project;
using PageTree.Server.Controllers;
using PageTree.Domain.Projects;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Corelibs.AspNetApi.Controllers;

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

        [HttpGet, Route_ID, AllowAnonymous]
        public Task<IActionResult> Get([FromRouteAndQuery] GetProjectApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetProjectQuery, GetProjectQueryOut>(query, _mapper);

        [HttpPost]
        public Task<IActionResult> Create([FromRouteAndBody] CreateProjectApiCommand command) =>
            _mediator.MapSendAndGetPutResponse<CreateProjectCommand>(command, _mapper);

        [HttpPut, Route_ID, Authorize_Edit_Project]
        public Task<IActionResult> Replace([FromRouteAndBody] EditProjectApiCommand command) =>
            _mediator.MapSendAndGetPutResponse<EditProjectCommand>(command, _mapper);
    }

    public class Authorize_Edit_ProjectAttribute : Authorize_EditAttribute
    {
        protected override string ResourceName => nameof(Project);
    }
}
