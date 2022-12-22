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
    public class PagesController : ControllerBase
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
        public Task<IActionResult> GetAll(GetPagesApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetPagesQuery, GetPagesQueryOut>(query, _mapper);

        [HttpPost, Action("create")]
        public Task<IActionResult> Create([FromBody] CreatePageApiCommand command = null) =>
            _mediator.SendAndGetPostResponse<CreatePageCommand>();

        [HttpDelete, Route("{id}"), Action("delete")]
        public Task<IActionResult> Delete(string id) =>
            _mediator.SendAndGetDeleteResponse(new DeletePageCommand(id));

        [HttpPatch, Route("{id}"), Action("change-name")]
        public Task<IActionResult> ChangeName(string id, string name) =>
            _mediator.SendAndGetPatchResponse(new ChangeNameOfPageCommand(id, name));

        [HttpPatch, Route("{id}"), Action("change-signature")]
        public Task<IActionResult> ChangeSignature(string id, string name) =>
            _mediator.SendAndGetPatchResponse(new ChangeSignatureOfPageCommand(id, name));
    }
}
