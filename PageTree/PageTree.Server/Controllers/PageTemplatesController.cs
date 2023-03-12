using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.PageTemplates.Commands;
using PageTree.App.PageTemplates.Queries;
using PageTree.Server.ApiContracts;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/pageTemplates")]
    [Authorize]
    public class PageTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PageTemplatesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, Route_ID, AllowAnonymous]
        public Task<IActionResult> Get([FromRouteAndQuery] GetPageTemplatesApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetPageTemplatesQuery, GetPageTemplatesQueryOut>(query, _mapper);

        [HttpDelete, Authorize_Edit]
        public Task<IActionResult> RemoveProperty([FromRouteAndBody] RemovePropertyTemplateApiCommand command) =>
            _mediator.MapSendAndGetDeleteResponse<RemovePropertyTemplateCommand>(command, _mapper);

        [HttpPatch, Route("{pageTemplateID}/changeName"), Authorize_Edit]
        public Task<IActionResult> ChangePageTemplateName([FromRouteAndBody] ChangeNameOfPageTemplateApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeNameOfPageTemplateCommand>(command, _mapper);
        
        [HttpPatch, Route("{pageTemplateID}/changePageName"), Authorize_Edit]
        public Task<IActionResult> ChangePageName([FromRouteAndBody] ChangeNameOfPageTemplatePageApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeNameOfPageTemplatePageCommand>(command, _mapper);

        [HttpPatch, Route("changeIndex"), Authorize_Edit]
        public Task<IActionResult> ChangeIndex([FromRouteAndBody] ChangeIndexOfPropertyTemplateApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeIndexOfPropertyTemplateCommand>(command, _mapper);

        [HttpPatch, Route("changeLevel"), Authorize_Edit]
        public Task<IActionResult> ChangeLevel([FromRouteAndBody] ChangeLevelOfPropertyTemplateApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeLevelOfPropertyTemplateCommand>(command, _mapper);

        [HttpPatch, Route("{pageTemplateID}/changeSignature"), Authorize_Edit]
        public Task<IActionResult> ChangeSignature([FromRouteAndBody] ChangeSignatureOfPageTemplateApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeSignatureOfPageTemplateCommand>(command, _mapper);
    }
}
