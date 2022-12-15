using Common.Infrastructure.MauiMsalAuth;
using Common.Infrastructure.MauiMsalAuth.Client;
using Corelibs.BlazorShared;
using Microsoft.AspNetCore.Components;

namespace PageTree.Client.Native.Auth
{
    internal class NativeSignInRedirector : MsalNativeSignInRedirector, ISignInRedirector
    {
        public NativeSignInRedirector(ISignInManager signInManager, NavigationManager navigation) : base(signInManager, navigation) {}
    }
}
