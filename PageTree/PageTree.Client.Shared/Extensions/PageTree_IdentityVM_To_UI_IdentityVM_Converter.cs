using Corelibs.BlazorViews.ViewModels;

namespace PageTree.Client.Shared.Extensions
{
    public static class PageTree_IdentityVM_To_UI_IdentityVM_Converter
    {
        public static IdentityVM ToUIIdentityVM(this App.Pages.Queries.IdentityVM identity) =>
            new(identity.ID, identity.Name);

        public static IdentityVM[] ToUIIdentityVM(this IEnumerable<App.Pages.Queries.IdentityVM> identities) =>
            identities.Select(id => id.ToUIIdentityVM()).ToArray();
    }
}
