using PageTree.App.Entities.Styles;
using System.Drawing;

namespace PageTree.App.UseCases.Pages.Queries.Styles
{
    internal partial class HardcodedStyles
    {
        private static Style RadicalsStyle = new()
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
                LayoutOfChildren = new()
                {
                    Type = LayoutType.Grid,
                    Gap = 20
                },
                VisualInfoOfChildren = new()
                {
                    BackgroundColor = new()
                    {
                        Default = Color.FromArgb(255, 255, 224, 234).ToArgb()
                    },
                    Padding = new()
                    {
                        All = 20
                    },
                    Font = new()
                    {
                        FontSize = 20,
                        FontWeight = FontWeight.Bold,
                    },
                    FontColor = new()
                    {
                        Default = Color.FromArgb(255, 255, 124, 154).ToArgb()
                    }
                },
                ChildrenStyle = new()
                {
                    new()
                    {
                        Type = StyleArtifactType.Name,
                    }
                },
                Children = new()
                {
                    new()
                    {
                        StyleType = ApplyStyleBy.Index,
                        ChildIndex = 0,
                        VisualInfo = new()
                        {
                            
                        },
                        Artifacts = new()
                        {
                            new()
                            {
                                Type = StyleArtifactType.Name
                            }
                        },
                        VisualInfoOfChildren = new()
                        {
                            BackgroundColor = new()
                            {
                                Default = Color.FromArgb(255, 255, 176, 193).ToArgb()
                            },
                            Padding = new()
                            {
                                All = 20
                            }
                        },
                        LayoutOfChildren = new()
                        {
                            Type = LayoutType.Grid,
                            Gap = 20
                        },
                    }
                }
            }
        };
    }
}
