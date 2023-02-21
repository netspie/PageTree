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
                AreChildrenExpanded = true
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
                        Default = Color.FromArgb(255, 200, 200, 200).ToArgb()
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
                Children = new()
                {
                    //new()
                    //{
                    //    StyleType = ApplyStyleBy.Index,
                    //    ChildIndex = 0,
                    //    VisualInfo = new()
                    //    {
                    //        Font = new()
                    //        {
                    //            FontSize = 30
                    //        }
                    //    },
                    //    Artifacts = new()
                    //    {
                    //        new()
                    //        {
                    //            Type = StyleArtifactType.Signature,
                    //            VisualInfo = new()
                    //            {
                    //                Font = new()
                    //                {
                    //                    FontSize = 8
                    //                }
                    //            }
                    //        }
                    //    },
                    //    VisualInfoOfChildren = new()
                    //    {
                    //        BackgroundColor = new()
                    //        {
                    //            Default = Color.FromArgb(255, 222, 222, 222).ToArgb()
                    //        },
                    //        Padding = new()
                    //        {
                    //            All = 20
                    //        }
                    //    },
                    //}
                }
            }
        };
    }
}
