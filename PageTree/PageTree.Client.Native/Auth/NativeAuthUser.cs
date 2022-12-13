using Common.Infrastructure.MauiMsalAuth;
using Common.Infrastructure.MauiMsalAuth.Client;
using PageTree.Client.Shared.Services;

namespace PageTree.Client.Native.Auth
{
    internal class NativeAuthUser : MsalNativeAuthUser, IAuthUser
    {
        public NativeAuthUser(ISignInManager signInManager) : base(signInManager) {}
    }
}
