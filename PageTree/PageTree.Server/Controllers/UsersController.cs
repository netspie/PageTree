using AutoMapper;
using Corelibs.AspNetApi.Controllers.ActionConstraints;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PageTree.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost, Action("create")]
        public Task<IActionResult> Create()
        {
            var claims = User.Claims.ToList();

            return Task.FromResult<IActionResult>(NoContent());
        }
    }
}
