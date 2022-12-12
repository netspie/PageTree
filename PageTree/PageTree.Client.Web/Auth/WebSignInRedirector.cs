using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Web.Auth
{
    public class WebSignInRedirector : ISignInRedirector
    {
        private readonly NavigationManager _navigation;

        public WebSignInRedirector(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        public void Redirect(Exception exception)
        {
            if (exception is AccessTokenNotAvailableException accessTokenException)
            {
                accessTokenException.Redirect();
                _navigation.NavigateTo(_navigation.Uri, forceLoad: true);
            }
        }
    }
}
