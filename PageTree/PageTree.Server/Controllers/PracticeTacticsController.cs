using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.UseCases.PracticeCategories.Commands;
using PageTree.Server.ApiContracts;

namespace PageTree.Server.Controllers
{
    namespace PageTree.Server.Api.Controllers
    {
        [ApiController]
        [Route("api/v1/practiceTactics")]
        [Authorize]
        public class PracticeTacticsController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public PracticeTacticsController(IMediator mediator, IMapper mapper)
            {
                _mediator = mediator;
                _mapper = mapper;
            }

            [HttpPost, Authorize_Edit]
            public Task<IActionResult> Create([FromRouteAndBody] CreatePracticeTacticApiCommand command = null) =>
                _mediator.MapSendAndGetPostResponse<CreatePracticeTacticCommand>(command, _mapper);

            [HttpDelete, Route("{practiceTacticID}"), Authorize_Edit]
            public Task<IActionResult> Delete([FromRouteAndBody] DeletePracticeTacticApiCommand command) =>
                _mediator.MapSendAndGetDeleteResponse<DeletePracticeTacticCommand>(command, _mapper);

            [HttpPatch, Route("{practiceTacticID}/changeName"), Authorize_Edit]
            public Task<IActionResult> ChangeName([FromRouteAndBody] ChangeNameOfPracticeTacticApiCommand command) =>
                _mediator.MapSendAndGetPatchResponse<ChangeNameOfPracticeTacticCommand>(command, _mapper);

            [HttpPatch, Route("{practiceTacticID}/changeIndex"), Authorize_Edit]
            public Task<IActionResult> ChangeIndex([FromRouteAndBody] ChangeIndexOfPracticeTacticApiCommand command) =>
                _mediator.MapSendAndGetPatchResponse<ChangeIndexOfPracticeTacticCommand>(command, _mapper);
        }
    }
}
