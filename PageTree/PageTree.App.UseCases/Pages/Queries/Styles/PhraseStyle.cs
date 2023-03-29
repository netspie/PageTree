using PageTree.App.Entities.Styles;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        private static Style PhraseStyle = new()
        {
            TreeExpandInfo = new()
            {
                IsExpanded = true,
                AreChildrenExpanded = true
            },

            RootProperty = new()
            {
                Layout = new()
                {
                    Type = LayoutType.Vertical,
                    Gap = 10
                },
                VisualInfoOfChildren = new()
                {
                    Visibility = Visibility.IfHasChildren,
                    Padding = new()
                    {
                        All = 20
                    },
                    Font = new()
                    {
                        FontSize = 12,
                        FontWeight = FontWeight.Bold,
                    },
                }
            }
        };
    }
}
