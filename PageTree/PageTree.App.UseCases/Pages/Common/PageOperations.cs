using Common.Basic.Blocks;
using Common.Basic.Repository;
using PageTree.App.Entities.Styles;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.UseCases.Pages.Common
{
    public static class PageExtensions
    {
        public static IdentityVM ToIdentityVM(this Page page) =>
            new(page.ID, page.Name);

        public static IdentityVM[] ToIdentityVMs(this IEnumerable<Page> pages) =>
            pages.Select(p => p.ToIdentityVM()).ToArray();

        public static PropertyType GetPropertyType(this Page parentPage, string propertyID)
        {
            if (parentPage.IsSubPage(propertyID))
                return PropertyType.Subpage;

            return PropertyType.Link;
        }
    }
}
