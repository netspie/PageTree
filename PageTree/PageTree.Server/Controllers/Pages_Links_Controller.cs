using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Pages.Commands;
using PageTree.Server.ApiContracts;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/pages/{pageID}/links")]
    [Authorize]
    public class Pages_Links_Controller : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public Pages_Links_Controller(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost, Authorize_Edit]
        public Task<IActionResult> Create([FromRouteAndBody] CreateLinkApiCommand command = null) =>
            _mediator.MapSendAndGetPostResponse<CreateLinkCommand>(command, _mapper);
    }
}
