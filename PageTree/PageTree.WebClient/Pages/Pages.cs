using Common.Basic.Maths;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PageTree.App.Pages.Queries;
using PageTree.App.Practice.Queries;
using PageTree.WebClient.Services;

namespace PageTree.WebClient.Pages
{
    public partial class Pages : ComponentBase
    {
        [Inject] private IPagesService PagesService { get; set; }

        private PageVM _vm = new PageVM();
        private PracticeCardDTO[] _practiceCards = Array.Empty<PracticeCardDTO>();
        private int _currentCardIndex;
        private bool _answerVisible;

        protected override Task OnInitializedAsync() =>
            //Exts.TryOrCatchAccessTokenNotAvailableAndRedirect(RefreshPage);
            RefreshPage();

        private async Task RefreshPage() =>
            _vm = await PagesService.GetPage(string.Empty);

        private async Task LoadPage(string pageID)
        {
            _vm = await PagesService.GetPage(string.Empty);
            await InvokeAsync(StateHasChanged);
        }

        private async Task LoadPracticeCard(string tacticID)
        {
            _practiceCards = await PagesService.GetPracticeCards(_vm.Identity.ID, tacticID);
            _currentCardIndex = 0;

            await InvokeAsync(StateHasChanged);
        }

        private void CloseCard()
        {
            _practiceCards = Array.Empty<PracticeCardDTO>();

            InvokeAsync(StateHasChanged);
        }

        private void NextCard(int step)
        {
            if (step == 0)
                return;

            _answerVisible = false;
            _currentCardIndex.IncreaseBy(step, 0, _practiceCards.Length - 1, loop: false);

            InvokeAsync(StateHasChanged);
        }

        private void SwitchAnswerVisibility()
        {
            _answerVisible = !_answerVisible;

            InvokeAsync(StateHasChanged);
        }
    }

    public static class Exts
    {
        public static async Task TryOrCatchAccessTokenNotAvailableAndRedirect(Func<Task> asyncTask)
        {
            try
            {
                await asyncTask();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }
    }
}
