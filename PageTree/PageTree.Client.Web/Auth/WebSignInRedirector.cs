using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Web.Auth
{
    public class WebSignInRedirector : ISignInRedirector
    {
        public void Redirect(Exception exception)
        {
            if (exception is AccessTokenNotAvailableException accessTokenException)
                accessTokenException.Redirect();
        }
    }
}
