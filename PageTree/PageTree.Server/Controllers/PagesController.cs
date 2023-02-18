using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Pages.Queries;
using PageTree.App.Projects.Commands;
using PageTree.Domain;
using PageTree.Server.ApiContracts;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/pages")]
    [Authorize]
    public class PagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PagesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, Route_ID, AllowAnonymous]
        public Task<IActionResult> Get([FromRouteAndQuery] GetPageApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetPageQuery, GetPageQueryOut>(query, _mapper);

        [HttpGet]
        public Task<IActionResult> GetAll([FromQuery] GetPagesApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetPagesQuery, GetPagesQueryOut>(query, _mapper);

        [HttpDelete, Route_ID, Action_Delete]
        public Task<IActionResult> Delete([FromQuery] DeletePageApiCommand command) =>
            _mediator.MapSendAndGetDeleteResponse<CreatePageCommand>(command, _mapper);

        [HttpPatch, Route("{pageID}"), Authorize_Edit]
        public Task<IActionResult> ChangeName([FromRouteAndBody] UpdatePageApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<UpdatePageCommand>(command, _mapper);
    }
}
