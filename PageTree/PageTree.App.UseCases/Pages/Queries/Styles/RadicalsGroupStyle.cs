using PageTree.App.Entities.Styles;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        private static Style RadicalsGroupStyle = new()
        {
            TreeExpandInfo = new()
            {
                IsExpanded = true,
            },
            
            RootProperty = new()
            {
                Layout = new()
                {
                    Type = LayoutType.Grid,
                    Gap = 20
                },
                LayoutOfChildren = new()
                {
                    Type = LayoutType.Grid,
                    Gap = 20
                },
                VisualInfoOfChildren = new()
                {
                    Padding = new()
                    {
                        All = 20
                    },
                },
            }
        };
    }
}
