using AutoMapper;
using PageTree.App.Pages.Queries;

namespace PageTree.Server.ApiContracts
{
    public class Pages_ApiToApp_MapProfile : Profile
    {
        public Pages_ApiToApp_MapProfile()
        {
            CreateMap<GetPageApiQuery, GetPageQuery>();
            CreateMap<CreatePageApiCommand, CreatePageCommand>();
        }
    }
}
