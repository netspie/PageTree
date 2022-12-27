using AutoMapper;
using Corelibs.AspNetApi.Controllers.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.ProjectUserLists.Queries;
using PageTree.Server.ApiContracts.Pages;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProjectUserListsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectUserListsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}"), AllowAnonymous]
        public Task<IActionResult> Get([FromQuery] GetProjectUserListApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetProjectUserListQuery, GetProjectUserListQueryOut>(query, _mapper);
    }
}
