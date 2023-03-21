using AutoMapper;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.UseCases.PracticeCategories.Queries;
using PageTree.Server.ApiContracts;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/projects/{projectID}/practiceCategories")]
    [Authorize]
    public class PracticeCategoriesOfProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PracticeCategoriesOfProjectController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, AllowAnonymous]
        public Task<IActionResult> Get([FromRouteAndQuery] GetProjectPracticeCategoriesApiQuery query) =>
            _mediator.MapSendAndGetResponse<GetProjectPracticeCategoriesQuery, GetProjectPracticeCategoriesQueryOut>(query, _mapper);
    }
}
