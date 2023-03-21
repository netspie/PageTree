using AutoMapper;
using Corelibs.AspNetApi.Authorization;
using Corelibs.AspNetApi.Controllers.Extensions;
using Corelibs.AspNetApi.ModelBinders;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.UseCases.Signatures.Commands;
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
        }
    }
}
