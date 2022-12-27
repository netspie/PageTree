using AutoMapper;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Pages.Queries;
using PageTree.Server.ApiContracts.Pages;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class ProjectSetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PagesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, Route("{id}"), AllowAnonymous]
        public Task<IActionResult> Get([FromQuery] GetPageApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetPageOfIDQuery, GetPageOfIDQueryOut>(query, _mapper);

        [HttpGet]
        public Task<IActionResult> GetAll([FromQuery] GetPagesApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetPagesQuery, GetPagesQueryOut>(query, _mapper);

        [HttpPost, Action("create")]
        public Task<IActionResult> Create([FromBody] CreatePageApiCommand command = null) =>
            _mediator.MapSendAndGetPostResponse<CreatePageCommand>(command, _mapper);

        [HttpDelete, Route("{id}"), Action("delete")]
        public Task<IActionResult> Delete([FromQuery] DeletePageApiCommand command) =>
            _mediator.MapSendAndGetDeleteResponse<CreatePageCommand>(command, _mapper);

        [HttpPatch, Route("{id}"), Action("change-name")]
        public Task<IActionResult> ChangeName([FromBody] ChangePageNameApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeNameOfPageCommand>(command, _mapper);

        [HttpPatch, Route("{id}"), Action("change-signature")]
        public Task<IActionResult> ChangeSignature([FromBody] ChangePageSignatureApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeSignatureOfPageCommand>(command, _mapper);
    }
}
