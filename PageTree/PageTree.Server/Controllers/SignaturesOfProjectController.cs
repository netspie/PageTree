using AutoMapper;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.UseCases.Signatures.Queries;
using PageTree.Server.ApiContracts;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/projects/{projectID}/signatures")]
    [Authorize]
    public class SignaturesOfProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SignaturesOfProjectController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, AllowAnonymous]
        public Task<IActionResult> Get([FromRouteAndQuery] GetProjectSignaturesApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetProjectSignaturesQuery, GetProjectSignaturesQueryOut>(query, _mapper);

        //[HttpDelete, Route_ID, Action_Delete]
        //public Task<IActionResult> Delete([FromQuery] DeletePageApiCommand command) =>
        //    _mediator.MapSendAndGetDeleteResponse<CreatePageCommand>(command, _mapper);

        //[HttpPatch, Route("{pageID}"), Authorize_Edit_Signature]
        //public Task<IActionResult> ChangeName([FromRouteAndBody] UpdatePageApiCommand command) =>
        //    _mediator.MapSendAndGetPatchResponse<UpdatePageCommand>(command, _mapper);
    }
}
