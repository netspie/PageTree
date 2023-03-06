using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Pages.Commands;
using PageTree.App.Pages.Queries;
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

        [HttpGet, Route("search"), AllowAnonymous]
        public Task<IActionResult> Search([FromRouteAndQuery] GetPagesSearchResultsApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetPagesSearchResultsQuery, GetPagesSearchResultsQueryOut>(query, _mapper);

        [HttpDelete, Authorize_Edit]
        public Task<IActionResult> RemoveProperty([FromRouteAndBody] RemovePropertyApiCommand command) =>
            _mediator.MapSendAndGetDeleteResponse<RemovePropertyCommand>(command, _mapper);

        [HttpPatch, Route("{pageID}/changeName"), Authorize_Edit]
        public Task<IActionResult> ChangeName([FromRouteAndBody] ChangeNameOfPageApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeNameOfPageCommand>(command, _mapper);

        [HttpPatch, Route("changeIndex"), Authorize_Edit]
        public Task<IActionResult> ChangeIndex([FromRouteAndBody] ChangeIndexOfPropertyApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeIndexOfPropertyCommand>(command, _mapper);

        [HttpPatch, Route("changeLevel"), Authorize_Edit]
        public Task<IActionResult> ChangeLevel([FromRouteAndBody] ChangeLevelOfPropertyApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeLevelOfPropertyCommand>(command, _mapper);

        [HttpPatch, Route("{pageID}/changeSignature"), Authorize_Edit]
        public Task<IActionResult> ChangeSignature([FromRouteAndBody] ChangeSignatureOfPageApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeSignatureOfPageCommand>(command, _mapper);
    }
}
