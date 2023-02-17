using PageTree.App.UseCases.Signatures.Commands;
using PageTree.App.UseCases.Signatures.Queries;

namespace PageTree.Server.ApiContracts
{
    public class Signatures_ApiToApp_MapProfile : BaseProfile
    {
        public Signatures_ApiToApp_MapProfile()
        {
            CreateMapCirc<CreateSignatureApiCommand, CreateSignatureCommand>();
            CreateMapCirc<GetProjectSignaturesApiQuery, GetProjectSignaturesQuery>();
        }
    }
}
