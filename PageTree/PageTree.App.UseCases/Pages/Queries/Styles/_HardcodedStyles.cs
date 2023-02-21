using PageTree.App.Entities.Styles;
using PageTree.App.Pages.Queries;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        public static Style GetMainStyle(IdentityVM page, IdentityVM signature)
        {
            Style result = null;

            result = page.Name switch
            {
                "Japanese Language" => DefaultStyle,
                _ => null
            };

            if (result != null)
                return result;

            result = signature.Name switch
            {
                "Author" => DefaultStyle,
                _ => null
            };

            return result;
        }
    }
}
