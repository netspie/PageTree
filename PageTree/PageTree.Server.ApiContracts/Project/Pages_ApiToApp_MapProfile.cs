using AutoMapper;
using PageTree.App.Projects.Queries;
using PageTree.Server.ApiContracts.Project;

namespace PageTree.Server.ApiContracts.Projects
{
    public class Projects_ApiToApp_MapProfile : Profile
    {
        public Projects_ApiToApp_MapProfile()
        {
            CreateMap<GetProjectApiQuery, GetProjectOfIDQuery>();
        }
    }
}
