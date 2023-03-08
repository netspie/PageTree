using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.PageTemplates.Commands;
using PageTree.Server.ApiContracts;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/pageTemplates/{templatePageID}/subPages")]
    [Authorize]
    public class PageTemplates_SubPages_Controller : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PageTemplates_SubPages_Controller(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost, Authorize_Edit]
        public Task<IActionResult> Create([FromRouteAndBody] CreateSubPageTemplateApiCommand command = null) =>
            _mediator.MapSendAndGetPostResponse<CreateSubPageTemplateCommand>(command, _mapper);
    }
}
