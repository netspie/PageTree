using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PageTree.App.Pages.Queries;

namespace PageTree.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PagesController : ControllerBase
    {
        //[HttpGet]
        //public ActionResult<PageVM[]> GetAll()
        //{
        //    return new[] { new PageVM() };
        //}

        [HttpGet]
        [Authorize(Policy = "AllowedForPageEdit")]
        public ActionResult<GetPageOfIDQueryDTO> Get(string id)
        {
            var claims = User.Claims.ToArray();
            return Ok(new GetPageOfIDQueryDTO(new PageVM()));
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
