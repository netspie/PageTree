using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Projects.Commands;
using PageTree.Domain;
using PageTree.Server.ApiContracts;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/pages/{parentID}/subPages")]
    [Authorize]
    public class PagesSubController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PagesSubController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost, Authorize_Edit]
        public Task<IActionResult> Create([FromRouteAndBody] CreateSubPageApiCommand command = null) =>
            _mediator.MapSendAndGetPostResponse<CreateSubPageCommand>(command, _mapper);
    }
}
