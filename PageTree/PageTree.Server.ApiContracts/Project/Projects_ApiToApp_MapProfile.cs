using PageTree.App.Projects.Commands;
using PageTree.App.Projects.Queries;

namespace PageTree.Server.ApiContracts
{
    public class Projects_ApiToApp_MapProfile : BaseProfile
    {
        public Projects_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetProjectApiQuery, GetProjectQuery>();
            CreateMapCirc<CreateProjectApiCommand, CreateProjectCommand>();
            CreateMapCirc<EditProjectApiCommand, EditProjectCommand>();
            CreateMapCirc<ArchiveProjectApiCommand, ArchiveProjectCommand>();
        }
    }
}
