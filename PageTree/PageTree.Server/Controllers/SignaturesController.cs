using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpDelete, Route("{signatureID}"), Authorize_Edit]
        public Task<IActionResult> Delete([FromRouteAndQuery] DeleteSignatureApiCommand command) =>
            _mediator.MapSendAndGetDeleteResponse<DeleteSignatureCommand>(command, _mapper);

        [HttpPatch, Route("{signatureID}"), Authorize_Edit]
        public Task<IActionResult> ChangeName([FromRouteAndBody] ChangeNameOfSignatureApiCommand command) =>
            _mediator.MapSendAndGetPatchResponse<ChangeNameOfSignatureCommand>(command, _mapper);
    }
}
