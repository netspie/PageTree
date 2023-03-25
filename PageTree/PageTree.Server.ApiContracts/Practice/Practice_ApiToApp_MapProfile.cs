using PageTree.App.UseCases.Practice.Queries;

namespace PageTree.Server.ApiContracts
{
    public class Practice_ApiToApp_MapProfile : BaseProfile
    {
        public Practice_ApiToApp_MapProfile()
        {
            CreateMapCirc<GetPracticeCardItemsApiQuery, GetPracticeCardItemsQuery>();
        }
    }
}
