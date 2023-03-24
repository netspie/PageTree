using PageTree.App.UseCases.PracticeTactics.Commands;
using PageTree.App.UseCases.PracticeTactics.Queries;

namespace PageTree.Server.ApiContracts
{
    public class PracticeTactics_ApiToApp_MapProfile : BaseProfile
    {
        public PracticeTactics_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetProjectPracticeTacticsApiQuery, GetProjectPracticeTacticsQuery>();
            CreateMapCirc<CreatePracticeTacticApiCommand, CreatePracticeTacticCommand>();
            CreateMapCirc<DeletePracticeTacticApiCommand, DeletePracticeTacticCommand>();
            CreateMapCirc<ChangeNameOfPracticeTacticApiCommand, ChangeNameOfPracticeTacticCommand>();
            CreateMapCirc<ChangeIndexOfPracticeTacticApiCommand, ChangeIndexOfPracticeTacticCommand>();
            CreateMapCirc<UpdateDataOfPracticeTacticApiCommand, UpdateDataOfPracticeTacticCommand>();
            CreateMapCirc<PracticeTacticItemApiVM, PracticeTacticItemVM>();
            CreateMapCirc<PageItemIDsApiVM, PageItemIDsVM>();
        }
    }
}
