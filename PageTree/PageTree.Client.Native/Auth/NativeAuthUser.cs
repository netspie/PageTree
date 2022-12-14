using Common.Infrastructure.MauiMsalAuth;
using Common.Infrastructure.MauiMsalAuth.Client;
using PageTree.Client.Shared.Auth;

namespace PageTree.Client.Native.Auth
{
    internal class NativeAuthUser : MsalNativeAuthUser, IAuthUser
    {
        public NativeAuthUser(ISignInManager signInManager) : base(signInManager) {}
    }
}
