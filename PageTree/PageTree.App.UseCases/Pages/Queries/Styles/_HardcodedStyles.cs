using PageTree.App.Entities.Styles;
using PageTree.App.UseCases.Common;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        public static Style GetMainStyle(IdentityVM page, IdentityVM signature)
        {
            Style result = RadicalsStyle;

            // By Page
            result = page.Name switch
            {
                "Japanese Language" => DefaultStyle,
                "Radicals" => RadicalsStyle,

                "Beginner" => RadicalsGroupStyle,
                "Apprentice" => RadicalsGroupStyle,
                "Regular" => RadicalsGroupStyle,
                "Advanced" => RadicalsGroupStyle,
                "Expert" => RadicalsGroupStyle,
                "Master" => RadicalsGroupStyle,
                "Supreme" => RadicalsGroupStyle,

                _ => null
            };

            if (result != null)
                return result;

            // By Signature
            result = signature.Name switch
            {
                "Author" => DefaultStyle,
                "Radical" => RadicalsGroupStyle,
                _ => null
            };

            return result;
        }
    }
}
