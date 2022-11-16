using PageTree.App.Pages.Queries;
using PageTree.App.Practice.Queries;

namespace PageTree.Presentation.Services
{
    public interface IPagesService
    {
        Task<PageVM> GetPage(string id);
        Task<PracticeCardDTO[]> GetPracticeCards(string pageID, string practiceTacticID);
    }
}
