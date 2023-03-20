using PageTree.App.Entities.Styles;
using PageTree.App.UseCases.Common;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        public static Style GetMainStyle(IdentityVM page, IdentityVM signature)
        {
            Style result = null;

            // By Page
            result = page.Name switch
            {
                "Japanese Language" => DefaultStyle,
                "Radicals" => RadicalsStyle,
                _ => null
            };

            if (result != null)
                return result;

            // By Signature
            result = signature.Name switch
            {
                "Author" => DefaultStyle,
                _ => null
            };

            return result;
        }
    }
}
