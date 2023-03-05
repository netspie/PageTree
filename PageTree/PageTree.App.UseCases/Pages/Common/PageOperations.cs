using Common.Basic.Blocks;
using Common.Basic.Repository;
using PageTree.App.Entities.Styles;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.UseCases.Pages.Common
{
    public static class PageExtensions
    {
        public static async Task<Result> GetParentPages(
            this IRepository<Page> pageRepository, Page page, List<Page> pages)
        {
            var result = Result.Success();
            if (string.IsNullOrEmpty(page.ParentID))
                return result;

            var parentPage = await pageRepository.Get(page.ParentID, result);
            pages.Add(parentPage);
            await GetParentPages(pageRepository, parentPage, pages);

            return result;
        }

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
