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

    [ApiController]
    public abstract class StandardRoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StandardRoutesController(IMediator mediator) =>
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

        [HttpDelete, Route("{id}"), Action("delete")]
        public Task<IActionResult> Delete(string id) =>
            _mediator.SendAndGetPatchResponse(new DeletePageCommand(id));

        [HttpPut, Route("{id}"), Action("replace")]
        public Task<IActionResult> Replace(string id) =>
           _mediator.SendAndGetPutResponse(new ReplacePageCommand(id));
    }

    public interface IApiCommand
    {

    }

    public abstract class CreateApiCommand : IApiCommand
    {

    }

    public abstract class DeleteApiCommand : IApiCommand
    {
        public string ID { get; set; }
    }

    public abstract class ReplaceApiCommand : IApiCommand
    {
        public string ID { get; set; }
    }

    public abstract class ChangeNameApiCommand : IApiCommand
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }
}
