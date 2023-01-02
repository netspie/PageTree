using AutoMapper;
using Corelibs.AspNetApi.Controllers;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.ProjectUserLists.Queries;
using PageTree.Server.ApiContracts;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/projectUserLists")]
    [Authorize]
    public class ProjectUserListsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectUserListsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, AllowAnonymous]
        public Task<IActionResult> Get() =>
            _mediator.MapSendAndGetResponse<GetProjectUserListQuery, GetProjectUserListQueryOut>(new object(), _mapper);

        [HttpGet, Route("{id}"), AllowAnonymous]
        public Task<IActionResult> Get([FromRouteAndQuery] GetProjectUserListApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetProjectUserListQuery, GetProjectUserListQueryOut>(query, _mapper);
    }
}
