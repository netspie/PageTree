using PageTree.App.Entities.Styles;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        private static Style RadicalStyle = new()
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
                    Padding = new()
                    {
                        All = 20
                    },
                    Font = new()
                    {
                        FontSize = 12,
                        FontWeight = FontWeight.Bold,
                    },
                },
                ChildrenStyle = new()
                {
                    new()
                    {
                        ShowOnlyIfHasNoChildrenVisible = true,
                        Type = StyleArtifactType.Signature,
                        VisualInfo = new()
                        {
                            Font = new()
                            {
                                FontSize = 12,
                                FontWeight = FontWeight.Bold,
                            },
                        }
                    },
                    new()
                    {
                        ApplyStyleOnlyIfHasNoChildrenVisible = true,
                        Type = StyleArtifactType.Name,
                        VisualInfo = new()
                        {
                            Font = new()
                            {
                                FontSize = 14,
                                FontWeight = FontWeight.Light,
                            },
                        }
                    }
                },
                Children = new()
                {
                }
            }
        };
    }
}
