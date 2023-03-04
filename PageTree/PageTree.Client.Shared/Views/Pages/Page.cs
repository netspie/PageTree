using Common.Basic.Collections;
using Common.Basic.Functional;
using Corelibs.Basic.Colors;
using Corelibs.BlazorShared.UI;
using Corelibs.BlazorViews.Components;
using Corelibs.BlazorViews.Layouts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using PageTree.App.Entities.Styles;
using PageTree.App.Pages.Queries;
using System.Drawing;

namespace PageTree.Client.Shared.Views.Pages
{
    public partial class Page : IView<PageVM>
    {
        [Parameter] public PageVM Model { get; set; }

        [Parameter] public int Left { get; set; }
        [Parameter] public int Top { get; set; }

        [Parameter] public Func<TreeLayout.TreeNode, Task> OnPropertyClick { get; set; }
        [Parameter] public Func<Task> OnAddSubPageOnTop { get; set; }
        [Parameter] public Func<Task> OnAddSubPageOnBottom { get; set; }
        [Parameter] public Func<string, int, Task> OnAddSubPage { get; set; }
        [Parameter] public Func<string, int, Task> OnAddLink { get; set; }
        [Parameter] public Func<string, string, Task> OnPropertyRemove { get; set; }
        [Parameter] public Func<string, string, int, Task> OnPropertyMove { get; set; }
        [Parameter] public Func<string, string, Task<bool>> OnPropertyRename { get; set; }
        [Parameter] public Func<string, string, Task> OnPropertyResignature { get; set; }
        [Parameter] public SelectLinkWindow.OnInputChangedDelegate? OnSelectLinkInputChanged { get; set; }
        [Parameter] public SelectLinkWindow.OnSelectedDelegate? OnSelectLinkSelected { get; set; }

        [Parameter] public bool IsEditMode { get; set; } = true;

        private string _mainStyle => $"width: calc(100% - {Left}px); ";

        private Style _style => Model.StyleOfPage!;

        private Arrangements? _arrangements;
        private Arrangements_AddNew? _arrangements_AddNew;
        private TreeLayout? _treeLayout;
        private ChooseFromListWindow _signatureChangeWindow;
        private SelectLinkWindow _selectLinkWindow;

        private List<Corelibs.BlazorViews.ViewModels.IdentityVM> _properties = new();

        private TreeLayout.TreeNode? _treeNode;

        public Task RefreshViewModel()
        {
            _treeNode = GetTreeNode();
            _properties = _treeNode.Children.Select(n => n.Identity).ToList();

            return Task.CompletedTask;
        }

        public async Task RefreshView()
        {
            await RefreshViewModel();
            await InvokeAsync(StateHasChanged);
        }

        protected override void OnInitialized()
        {
            RefreshViewModel();
        }

        private void OnArrangmentsButton()
        {
            _arrangements.OuterClick.Enabled = !_arrangements.OuterClick.Enabled;
        }

        private void OnAddNewArrangmentClick()
        {
            _arrangements.OuterClick.Enabled = !_arrangements.OuterClick.Enabled;
            _arrangements_AddNew.OuterClick.Enabled = !_arrangements_AddNew.OuterClick.Enabled;
        }

        private Task<bool> BeforeExpand(string id)
        {
            return Task.FromResult(true);
        }

        private Task AfterExpand(string id) => InvokeAsync(StateHasChanged);

        private TreeLayout.TreeNode.GetContentDelegate GetProperty(
            PropertyVM propertyVM,
            string parentID,
            int propertyIndex,
            bool isLast,
            StyleOfRootProperty parentStyle,
            StyleOfChildProperty childStyle,
            Style[] signatureOrPageStyles)
        {
            return GetContent;
            RenderFragment GetContent(TreeLayout.TreeNode node, ref int seq)
            {
                int seqLocal = seq;
                seq += 4;

                return RenderFragmentExtensions.CreateComponent<Property>(builder =>
                {
                    var vmModel = GetPropertyViewModel(propertyVM, parentID, parentStyle, childStyle, propertyIndex, propertyVM.PropertyType);
                    // override main by signature or page style

                    builder.AddAttribute(seqLocal++, "Model", vmModel);
                    builder.AddAttribute(seqLocal++, "IsEditMode", IsEditMode);

                    builder.AddAttribute(seqLocal++, "OnCreateSubpage", OnCreateSubPageAfterPropertyInternal);
                    builder.AddAttribute(seqLocal++, "OnCreateLink", OnCreateLinkAfterPropertyInternal);

                    builder.AddAttribute(seqLocal++, "OnRemove", OnPropertyRemoveInternal);
                    builder.AddAttribute(seqLocal++, "OnMoveUp", OnPropertyMoveUpInternal);
                    builder.AddAttribute(seqLocal++, "OnMoveDown", OnPropertyMoveDownInternal);
                    builder.AddAttribute(seqLocal++, "OnRename", OnPropertyRename);
                    builder.AddAttribute(seqLocal++, "OnResignature", OnResignatureMenuButtonInternal);

                    builder.AddAttribute(seqLocal++, "Index", propertyIndex);
                    builder.AddAttribute(seqLocal++, "IsLast", isLast);
                });
            };
        }

        private static Property.ViewModel GetPropertyViewModel(
            PropertyVM propertyVM,
            string parentID,
            StyleOfRootProperty parentStyle,
            StyleOfChildProperty childStyle,
            int propertyIndex,
            PropertyType propertyType)
        {
            var vmModel = new Property.ViewModel();

            vmModel.ID = propertyVM.Identity.ID;
            vmModel.ParentID = parentID;
            vmModel.Signature = new()
            {
                ID = propertyVM?.SignatureIdentity?.ID,
                Name = propertyVM?.SignatureIdentity?.Name,
            };

            bool hasDefinedChildrenArtifacts = childStyle != null && !childStyle.Artifacts.IsNullOrEmpty();

            // apply parent style
            if (parentStyle != null && !parentStyle.ChildrenStyle.IsNullOrEmpty())
            {
                parentStyle.ChildrenStyle.ForEach(artifact =>
                {
                    var visualInfoVM = new Property.VisualInfoVM()
                        .OverrideBy(parentStyle.VisualInfoOfChildren)
                        .OverrideBy(artifact.VisualInfo);

                    var newArtifactVM = propertyVM.ToArtifactVM(artifact.Type, visualInfoVM);
                    vmModel.Artifacts.Add(newArtifactVM);
                });
            }
            else if (!hasDefinedChildrenArtifacts)
            {
                if (propertyVM.SignatureIdentity != null)
                    vmModel.Artifacts.Add(propertyVM.ToSignatureArtifactVM());

                vmModel.Artifacts.Add(propertyVM.ToNameArtifactVM());
            }

            // apply child style visual info
            if (childStyle != null && childStyle.VisualInfo != null && vmModel.Artifacts != null)
            {
                vmModel.Artifacts
                    //.Where(vmArtifact => childStyle.Artifacts
                    //.FirstOrDefault(artifact => ((Property.ArtifactType)artifact.Type) != vmArtifact.Type) != null)
                    .ForEach((vmArtifact, i) =>
                    {
                        vmArtifact.VisualInfo.OverrideBy(childStyle.VisualInfo);
                    });
            }

            // apply child style artifacts
            if (hasDefinedChildrenArtifacts)
            {
                childStyle.Artifacts.ForEach((artifact, i) =>
                {
                    var vmArtifactInfo = vmModel.Artifacts
                        .Select((a, i) => new { vmArtifact = a, index = i })
                        .FirstOrDefault(a => a.vmArtifact.Type == (Property.ArtifactType)artifact.Type);

                    if (vmArtifactInfo == null)
                    {
                        var newArtifactVM = propertyVM.ToArtifactVM(artifact.Type);

                        vmArtifactInfo = new { vmArtifact = newArtifactVM, index = vmModel.Artifacts.Count };
                        vmModel.Artifacts.Add(vmArtifactInfo.vmArtifact);
                    }

                    vmModel.Artifacts.Swap(vmArtifactInfo.index, i);
                    vmArtifactInfo.vmArtifact.VisualInfo
                        .OverrideBy(artifact.VisualInfo);
                });
            }

            return vmModel;
        }

        private void ModifyNode(RenderTreeBuilder builder, TreeLayout.TreeNode node, ref int sequence)
        {
            var styleData = node.Data as StyleData;

            //if (node.Parent != null && node.Parent.GetContent != null)
            //{
            //    builder
            //        .AddCssAttribute(ref sequence, "Padding", 4)
            //        .AddCssAttribute(ref sequence, "PaddingLeft", 10);
            //}
            //else
            //{
            //    builder.AddCssAttribute(ref sequence, "Padding", 8);
            //}

            builder.AddAttribute(sequence++, "OnClick", OnClick);
            Task OnClick() => OnPropertyClick?.InvokeIfOk(node);

            builder.AddCssAttribute(ref sequence, "BorderRadius", 10);

            if (styleData.Parent != null)
                Apply(styleData.Parent.VisualInfoOfChildren, ref sequence);

            if (styleData.Child != null)
                Apply(styleData.Child.VisualInfo, ref sequence);

            void Apply(PageTree.App.Entities.Styles.VisualInfo visualInfo, ref int sequence)
            {
                if (visualInfo?.BackgroundColor?.Default != null)
                    builder.AddCssAttribute(ref sequence, "Background", visualInfo.BackgroundColor.Default.ToColor().ToHexString());

                if (visualInfo?.Padding?.All != null)
                    builder.AddCssAttribute(ref sequence, "Padding", visualInfo.Padding.All.Value);

                if (visualInfo?.Padding?.Left != null)
                    builder.AddCssAttribute(ref sequence, "PaddingLeft", visualInfo.Padding.Left.Value);

                if (visualInfo?.Padding?.Right != null)
                    builder.AddCssAttribute(ref sequence, "PaddingRight", visualInfo.Padding.Right.Value);

                if (visualInfo?.Padding?.Top != null)
                    builder.AddCssAttribute(ref sequence, "PaddingTop", visualInfo.Padding.Top.Value);

                if (visualInfo?.Padding?.Bottom != null)
                    builder.AddCssAttribute(ref sequence, "PaddingBottom", visualInfo.Padding.Bottom.Value);
            }
        }

        private PageHeader.ViewModel GetPageHeader()
        {
            var vm = new PageHeader.ViewModel();

            if (Model!.SignatureIdentity != null && !Model!.SignatureIdentity.Name.IsNullOrEmpty())
                vm.Artifacts.Add(
                    new()
                    {
                        Text = Model.SignatureIdentity.Name,
                        Font = new() { FontSize = 16 }
                    });
            else
                vm.Artifacts.Add(
                    new()
                    {
                        Text = "",
                        Font = new() { FontSize = 16 }
                    });

            vm.Artifacts.Add(
                new()
                {
                    Text = Model.Identity.Name,
                    Font = new() { FontSize = 24 }
                });

            return vm;
        }

        private TreeLayout.TreeNode GetTreeNode()
        {
            Layout layout = null;
            if (Model != null && Model.StyleOfPage != null && Model?.StyleOfPage?.RootProperty != null)
                layout = Model.StyleOfPage.RootProperty.Layout;

            float layoutGap = 0;
            if (layout != null && layout.Gap.HasValue)
                layoutGap = layout.Gap.Value;

            return new()
            {
                CanExpand = false,
                IsExpanded = true,
                Children = GetTreeNodes(Model.Properties, Model.Identity.ID, Model?.StyleOfPage?.RootProperty, Model?.StylesOfChildren),
                Data = new StyleData(Model.StylesOfChildren, null, Model?.StyleOfPage?.RootProperty),
                Layout = new()
                {
                    Gap = layoutGap
                },
            };
        }

        private List<TreeLayout.TreeNode> GetTreeNodes(
            PropertyVM[] propertyVMs,
            string parentID,
            StyleOfRootProperty parentStyle,
            Style[] signatureOrPageStyles)
        {
            var list = new List<TreeLayout.TreeNode>();

            propertyVMs.ForEach((propertyVM, i) =>
            {
                var styleOfIndex = parentStyle?.Children?.FirstOrDefault(c => c.StyleType == PageTree.App.Entities.Styles.ApplyStyleBy.Index && c.ChildIndex == i);
                var styleOfType = parentStyle?.Children?.FirstOrDefault(c => c.StyleType == PageTree.App.Entities.Styles.ApplyStyleBy.PropertyType && propertyVM.PropertyType == c.PropertyType);
                var childStyle = styleOfType ?? styleOfIndex;

                var layout = parentStyle?.Layout;
                var layoutGap = layout != null && layout.Gap.HasValue ? layout.Gap.Value : 0;

                list.Add(new()
                {
                    Identity = new() { ID = propertyVM.Identity.ID, Name = propertyVM.Identity.Name },
                    CanExpand = IsEditMode ? propertyVM.CanExpand : false,
                    HasChildren = propertyVM.HasChildren,
                    IsExpanded = propertyVM.IsExpanded,
                    Layout = new()
                    {
                        Type = parentStyle?.LayoutOfChildren != null ? (TreeLayout.LayoutType)parentStyle?.LayoutOfChildren?.Type : TreeLayout.LayoutType.Vertical,
                        Gap = layoutGap
                    },

                    Children = GetTreeNodes(propertyVM.Properties, propertyVM.Identity.ID, childStyle, signatureOrPageStyles),
                    GetContent = GetProperty(
                        propertyVM,
                        parentID, 
                        propertyIndex: i, 
                        isLast: i == propertyVMs.Length - 1,
                        parentStyle, childStyle, signatureOrPageStyles),

                    Data = new StyleData(Model.StylesOfChildren, parentStyle, childStyle)
                });
            });

            return list;
        }

        private Task OnPropertyRemoveInternal(string propertyID) =>
            OnPropertyRemove?.Invoke(Model.Identity.ID, propertyID);

        private Task OnCreateSubPageAfterPropertyInternal(string pageID, int index) =>
            OnAddSubPage?.Invoke(pageID, index);

        private Task OnCreateLinkAfterPropertyInternal(string pageID, int index)
        {
            _selectLinkWindow.Show(Model.ProjectID, pageID, index);
            return Task.CompletedTask;
        }

        private class StyleData
        {
            public StyleOfRootProperty? Parent { get; }
            public StyleOfRootProperty? Child { get; }
            public Style[] StylesOfChildren { get; }

            public StyleData(
                Style[] stylesOfChildren,
                StyleOfRootProperty parent,
                StyleOfRootProperty child)
            {
                StylesOfChildren = stylesOfChildren;
                Parent = parent;
                Child = child;
            }
        }

        private Task OnPropertyMoveUpInternal(string parentPropertyOrPageID, string propertyID, int currentIndex) =>
            OnPropertyMove(parentPropertyOrPageID, propertyID, currentIndex - 1);

        private Task OnPropertyMoveDownInternal(string parentPropertyOrPageID, string propertyID, int currentIndex) =>
            OnPropertyMove(parentPropertyOrPageID, propertyID, currentIndex + 1);

        private Task OnResignatureMenuButtonInternal(string propertyID)
        {
            _signatureChangeWindow.ID = propertyID;
            _signatureChangeWindow.OuterClick.Enabled = true;
            return Task.CompletedTask;
        }

        private Task OnResignatureInternal(string propertyID, string signatureID)
        {
            _signatureChangeWindow.OuterClick.Enabled = false;
            OnPropertyResignature?.Invoke(propertyID, signatureID);
            return Task.CompletedTask;
        }

        public Task UpdateSelectLinkWindow(SearchedPagesResultsVM vm) => _selectLinkWindow.Update(vm);

        private readonly static Color BackgroundColor = Color.FromArgb(255, 225, 228, 228);
    }
}
