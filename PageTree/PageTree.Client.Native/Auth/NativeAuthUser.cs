using Common.Infrastructure.MauiMsalAuth;
using Common.Infrastructure.MauiMsalAuth.Client;
using Corelibs.BlazorShared;

namespace PageTree.Client.Native.Auth
{
    internal class NativeAuthUser : MsalNativeAuthUser, IAuthUser
    {
        public NativeAuthUser(ISignInManager signInManager) : base(signInManager) {}
    }
}
