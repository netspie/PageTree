using Common.Basic.Blocks;
using Corelibs.BlazorShared.UI;
using Corelibs.BlazorViews.Layouts;
using Mediator;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PageTree.Client.Shared.Interfaces;

namespace PageTree.Client.Shared.Pages
{
    public abstract class BasePage<TQuery, TQueryOut, TVM, TView> : BaseComponent<TQuery, TQueryOut, TVM, TView>
        where TQuery : IQuery<Result<TQueryOut>>
        where TVM : new ()
        where TView : IView<TVM>
    {
        [Inject] protected IJSRuntime _jsRuntime { get; set; }

        protected IBgAndContent _backgroundOwner;

        [CascadingParameter] public RefreshInvoker RefreshInvoker { get; set; }

        protected override void OnInitialized()
        {
            if (RefreshInvoker != null)
                RefreshInvoker.Action = () => InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_backgroundOwner == null)
                return;

            int left = 0;
            int top = 56;
            try
            {
                var menuRect = await _jsRuntime.GetRect("menu-topmost");
                left = (int)menuRect.Width;
            }
            catch (Exception) { }
            try
            {
                var topRect = await _jsRuntime.GetRect("navbar-menu-parent");
                top = (int)topRect.Height;
            }
            catch (Exception) { }

            _backgroundOwner.Resize(0, 0);
        }
    }
}
