using PageTree.App.Entities.Styles;
using System.Drawing;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        private static Style DefaultStyle = new()
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
                    Gap = 20
                },
                VisualInfoOfChildren = new()
                {
                    BackgroundColor = new()
                    {
                        Default = Color.FromArgb(255, 252, 252, 252).ToArgb()
                    },
                    Padding = new()
                    {
                        All = 20
                    }
                },
                ChildrenStyle = new()
                {
                    new()
                    {
                        Type = StyleArtifactType.Name,
                        VisualInfo = new()
                        {
                            Font = new()
                            {
                                FontSize = 18,
                                FontWeight = FontWeight.Bold
                            }
                        }
                    }
                },
            }
        };
    }
}
