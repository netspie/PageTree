using AutoMapper;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.UseCases.Practice.Queries;
using PageTree.Server.ApiContracts;

namespace PageTree.Server.Controllers
{
    namespace PageTree.Server.Api.Controllers
    {
        [ApiController]
        [Route("api/v1/practice")]
        [Authorize]
        public class PracticeController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public PracticeController(IMediator mediator, IMapper mapper)
            {
                _mediator = mediator;
                _mapper = mapper;
            }

            [HttpGet, Route("generate"), AllowAnonymous]
            public Task<IActionResult> Search([FromRouteAndQuery] GetPracticeCardItemsApiQuery query) =>
                _mediator.MapSendAndGetResponse<GetPracticeCardItemsQuery, GetPracticeCardItemsQueryOut>(query, _mapper);
        }
    }
}
