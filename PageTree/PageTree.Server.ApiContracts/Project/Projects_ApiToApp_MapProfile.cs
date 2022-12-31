using PageTree.App.Projects.Commands;
using PageTree.App.Projects.Queries;
using PageTree.Server.ApiContracts.Common;
using PageTree.Server.ApiContracts.Project;

namespace PageTree.Server.ApiContracts.Projects
{
    public class Projects_ApiToApp_MapProfile : BaseProfile
    {
        public Projects_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetProjectApiQuery, GetProjectQuery>();
            CreateMapCirc<CreateProjectApiCommand, CreateProjectCommand>();
            CreateMapCirc<EditProjectApiCommand, EditProjectCommand>();
        }
    }
}
