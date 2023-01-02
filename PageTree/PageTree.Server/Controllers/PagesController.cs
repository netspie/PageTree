using AutoMapper;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public Task<IActionResult> GetAll([FromQuery] GetPagesApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetPagesQuery, GetPagesQueryOut>(query, _mapper);

        [HttpPost, Action_Create]
        public Task<IActionResult> Create([FromBody] CreatePageApiCommand command = null) =>
            _mediator.MapSendAndGetPostResponse<CreatePageCommand>(command, _mapper);

        [HttpDelete, Route_ID, Action_Delete]
        public Task<IActionResult> Delete([FromQuery] DeletePageApiCommand command) =>
            _mediator.MapSendAndGetDeleteResponse<CreatePageCommand>(command, _mapper);

        [HttpPut, Route("{name}")]
        public Task<IActionResult> ChangeName([FromBody] ChangePageNameApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeNameOfPageCommand>(command, _mapper);

        [HttpPut, Route("{signature}")]
        public Task<IActionResult> ChangeSignature([FromBody] ChangePageSignatureApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeSignatureOfPageCommand>(command, _mapper);
    }
}
