using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Pages.Queries;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class PagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PagesController(IMediator mediator) =>
            _mediator = mediator;

        [HttpGet]
        public Task<IActionResult> Get() => 
            _mediator.SendAndGetResponse(new GetPagesQuery());

        [HttpGet, Route("{id}"), AllowAnonymous]
        public Task<IActionResult> Get(string id) => 
            _mediator.SendAndGetResponse(new GetPageOfIDQuery(id));

        [HttpPost, Action("create")]
        public Task<IActionResult> Create() =>
            _mediator.SendAndGetPostResponse<CreatePageCommand>();

        [HttpPatch, Route("{id}"), Action("delete")]
        public Task<IActionResult> Delete(string id) =>
            _mediator.SendAndGetDeleteResponse(new DeletePageCommand(id));

        [HttpPatch, Route("{id}"), Action("change-name")]
        public Task<IActionResult> ChangeName(string id, string name) =>
            _mediator.SendAndGetDeleteResponse(new ChangeNameOfPageCommand(id, name));

        [HttpPatch, Route("{id}"), Action("change-signature")]
        public Task<IActionResult> ChangeSignature(string id, string name) =>
            _mediator.SendAndGetDeleteResponse(new ChangeSignatureOfPageCommand(id, name));
    }
}
