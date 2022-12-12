using Common.Infrastructure.MauiMsalAuth;
using Microsoft.AspNetCore.Components;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Native.Auth
{
    internal class NativeSignInRedirector : ISignInRedirector
    {
        private readonly ISignInManager _signInManager;
        private readonly NavigationManager _navigation;

        public NativeSignInRedirector(ISignInManager signInManager, NavigationManager navigation)
        {
            _signInManager = signInManager;
            _navigation = navigation;
        }

        public async void Redirect(Exception exception)
        {
            if (exception is NoAccessTokenAvailableException)
            {
                await _signInManager.SignIn();
                _navigation.NavigateTo("/", forceLoad: true);
            }
        }
    }
}
