using PageTree.App.Entities.Styles;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        private static Style PhrasesStyle = new()
        {
            TreeExpandInfo = new()
            {
                IsExpanded = true,
                AreChildrenExpanded = false
            },

            RootProperty = new()
            {
                Layout = new()
                {
                    Type = LayoutType.Vertical,
                },
                VisualInfoOfChildren = new()
                {
                    Font = new()
                    {
                        FontSize = 15,
                    },
                }
            }
        };
    }
}
