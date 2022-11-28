using Mediator;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Pages.Queries;

namespace PageTree.Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class PagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PagesController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public ActionResult<PageVM[]> GetAll()
        //{
        //    return new[] { new PageVM() };
        //}

        [HttpGet]
        //[Authorize(Policy = "AllowedForPageEdit")]
        public async Task<ActionResult<GetPageOfIDQueryDTO>> Get(string id)
        {
            var result = await _mediator.Send(new GetPageOfIDQuery(id));
            return Ok(result.Get());
        }

        //[HttpPost]
        //[Route("{id}/subpages")]
        //public IActionResult CreateSubpage(string pageID, [FromBody] CreateSubPageCommand command)
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //[Route("{id}/links")]
        //public IActionResult CreateLink(string pageID, [FromBody] CreateLinkCommand command)
        //{
        //    return Ok();
        //}

        //[HttpDelete]
        //[Route("{id}/{childID}")]
        //public IActionResult RemoveProperty(string pageID, string childID)
        //{
        //    return Ok();
        //}

        //[HttpDelete]
        //public IActionResult Delete()
        //{
        //    return Ok();
        //}

        public class CreateSubPageCommand
        {
            public string Name { get; }
            public PropertyPositionType PositionType { get; }
            public int PositionIndex { get; }
        }

        public class CreateLinkCommand
        {
            public string LinkID { get; }
            public PropertyPositionType PositionType { get; }
            public int PositionIndex { get; }
        }

        public enum PropertyPositionType
        {
            Custom,
            Top,
            Bottom,
            Middle,
        }
    }
}
