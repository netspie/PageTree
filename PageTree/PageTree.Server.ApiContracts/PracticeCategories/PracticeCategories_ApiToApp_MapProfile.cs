using PageTree.App.UseCases.PracticeCategories.Queries;

namespace PageTree.Server.ApiContracts
{
    public class PracticeCategories_ApiToApp_MapProfile : BaseProfile
    {
        public PracticeCategories_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetProjectPracticeCategoriesApiQuery, GetProjectPracticeCategoriesQuery>();
        }
    }
}
