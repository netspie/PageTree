using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using DnsClient.Internal;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Entities.Signatures;
using PageTree.App.UseCases.Signatures.Commands;
using PageTree.Server.ApiContracts;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/signatures")]
    [Authorize]
    public class SignaturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SignaturesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost, Authorize_Edit]
        public Task<IActionResult> Create([FromRouteAndBody] CreateSignatureApiCommand command = null) =>
            _mediator.MapSendAndGetPostResponse<CreateSignatureCommand>(command, _mapper);

        //[HttpGet, Route_ID, AllowAnonymous]
        //public Task<IActionResult> Get([FromRouteAndQuery] GetProjectSignaturesApiQuery query) =>
        //    _mediator.MapSendAndGetResponse<GetProjectSignaturesQuery, GetProjectSignaturesQueryOut>(query, _mapper);

        //[HttpDelete, Route_ID, Action_Delete]
        //public Task<IActionResult> Delete([FromQuery] DeletePageApiCommand command) =>
        //    _mediator.MapSendAndGetDeleteResponse<CreatePageCommand>(command, _mapper);

        //[HttpPatch, Route("{pageID}"), Authorize_Edit_Signature]
        //public Task<IActionResult> ChangeName([FromRouteAndBody] UpdatePageApiCommand command) =>
        //    _mediator.MapSendAndGetPatchResponse<UpdatePageCommand>(command, _mapper);
    }
}
