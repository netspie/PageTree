using Common.Infrastructure.MauiMsalAuth;
using Common.Infrastructure.MauiMsalAuth.Client;
using Microsoft.AspNetCore.Components;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Native.Auth
{
    internal class NativeSignInRedirector : MsalNativeSignInRedirector, ISignInRedirector
    {
        public NativeSignInRedirector(ISignInManager signInManager, NavigationManager navigation) : base(signInManager, navigation) {}
    }
}
