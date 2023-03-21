using PageTree.App.UseCases.PracticeCategories.Commands;
using PageTree.App.UseCases.PracticeCategories.Queries;

namespace PageTree.Server.ApiContracts
{
    public class PracticeCategories_ApiToApp_MapProfile : BaseProfile
    {
        public PracticeCategories_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetProjectPracticeCategoriesApiQuery, GetProjectPracticeCategoriesQuery>();
            CreateMapCirc<CreatePracticeCategoryApiCommand, CreatePracticeCategoryCommand>();
            CreateMapCirc<DeletePracticeCategoryApiCommand, DeletePracticeCategoryCommand>();
            CreateMapCirc<ChangeNameOfPracticeCategoryApiCommand, ChangeNameOfPracticeCategoryCommand>();
            CreateMapCirc<ChangeIndexOfPracticeCategoryApiCommand, ChangeIndexOfPracticeCategoryCommand>();
        }
    }
}
