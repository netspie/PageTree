using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Web.Auth
{
    public class WebAuthUser : IAuthUser
    {
        private readonly NavigationManager _navigation;
        private readonly SignOutSessionStateManager _signOutManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        private bool? _isSignedIn = false;
        public Task<bool> IsSignedIn() => Task.FromResult(_isSignedIn.Value);

        private string _name = string.Empty;
        public string Name => _name;

        public event Action<bool> OnAuthenticatedStateChanged;

        public WebAuthUser(
            NavigationManager navigation,
            SignOutSessionStateManager signOutManager,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _navigation = navigation;
            _signOutManager = signOutManager;
            _authenticationStateProvider = authenticationStateProvider;

            _authenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var state = await task;
            _isSignedIn = state.User.Identity?.IsAuthenticated;
            
            if (_isSignedIn.Value)
                _name = state.User.Identity?.Name ?? string.Empty;

            OnAuthenticatedStateChanged?.Invoke(_isSignedIn.Value);
        }

        public Task SignIn()
        {
            _navigation.NavigateTo("authentication/login");
            return Task.CompletedTask;
        }

        public async Task SignOut()
        {
            await _signOutManager.SetSignOutState();
            _navigation.NavigateTo("authentication/logout");
        }
    }
}
