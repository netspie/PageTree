using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PageTree.Server.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public string UserID 
        {
            get 
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idClaim == null)
                    return null;

                return idClaim.Value;
            }
        }

    }
}
