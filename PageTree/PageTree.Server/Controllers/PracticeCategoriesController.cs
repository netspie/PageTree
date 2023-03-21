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
        [Route("api/v1/practiceCategories")]
        [Authorize]
        public class PracticeCategoriesController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public PracticeCategoriesController(IMediator mediator, IMapper mapper)
            {
                _mediator = mediator;
                _mapper = mapper;
            }

            [HttpPost, Authorize_Edit]
            public Task<IActionResult> Create([FromRouteAndBody] CreatePracticeCategoryApiCommand command = null) =>
                _mediator.MapSendAndGetPostResponse<CreatePracticeCategoryCommand>(command, _mapper);

            [HttpDelete, Route("{practiceCategoryID}"), Authorize_Edit]
            public Task<IActionResult> Delete([FromRouteAndBody] DeletePracticeCategoryApiCommand command) =>
                _mediator.MapSendAndGetDeleteResponse<DeletePracticeCategoryCommand>(command, _mapper);

            [HttpPatch, Route("{practiceCategoryID}/changeName"), Authorize_Edit]
            public Task<IActionResult> ChangeName([FromRouteAndBody] ChangeNameOfPracticeCategoryApiCommand command) =>
                _mediator.MapSendAndGetPatchResponse<ChangeNameOfPracticeCategoryCommand>(command, _mapper);

            [HttpPatch, Route("{practiceCategoryID}/changeIndex"), Authorize_Edit]
            public Task<IActionResult> ChangeIndex([FromRouteAndBody] ChangeIndexOfPracticeCategoryApiCommand command) =>
                _mediator.MapSendAndGetPatchResponse<ChangeIndexOfPracticeCategoryCommand>(command, _mapper);
        }
    }
}
