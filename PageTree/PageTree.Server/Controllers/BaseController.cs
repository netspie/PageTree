using Microsoft.AspNetCore.Mvc;

namespace PageTree.Server.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public string UserID => User.GetUserID();
    }
}
