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
                        Type = StyleArtifactType.Name,
                    }
                },
                Children = new()
                {
                    new()
                    {
                        //StyleType = ApplyStyleBy.s,
                        //ChildIndex = 0,
                        //VisualInfo = new()
                        //{

                        //},
                        //Artifacts = new()
                        //{
                        //    new()
                        //    {
                        //        Type = StyleArtifactType.Name
                        //    }
                        //},
                        //VisualInfoOfChildren = new()
                        //{
                        //    Padding = new()
                        //    {
                        //        All = 20
                        //    }
                        //},
                        //LayoutOfChildren = new()
                        //{
                        //    Type = LayoutType.Grid,
                        //    Gap = 20
                        //},
                    },
                }
            }
        };
    }
}
