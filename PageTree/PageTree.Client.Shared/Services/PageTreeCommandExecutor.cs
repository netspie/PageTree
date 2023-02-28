using AutoMapper;
using Corelibs.BlazorShared;
using PageTree.App.Pages.Commands;
using PageTree.App.Projects.Commands;
using PageTree.App.UseCases.Signatures.Commands;
using PageTree.App.UseCases.Users.Commands;
using PageTree.Server.ApiContracts;

namespace PageTree.Client.Shared.Services
{
    public class PageTreeCommandExecutor : CommandRequestExecutor
    {
        public PageTreeCommandExecutor(IMapper mapper, IHttpClientFactory clientFactory, ISignInRedirector signInRedirector)
            : base("/api/v1", mapper, clientFactory, signInRedirector)
        {
            // Users
            AddPost<CreateUserCommand>("users");

            // Projects
            AddPost<CreateProjectCommand, CreateProjectApiCommand>("projects");
            AddDelete<ArchiveProjectCommand, ArchiveProjectApiCommand>("projects/{id}");
            AddPut<EditProjectCommand, EditProjectApiCommand>("projects");

            // Pages
            AddPost<CreateSubPageCommand, CreateSubPageApiCommand>("pages/{parentID}/subpages");
            AddDelete<RemovePropertyCommand, RemovePropertyApiCommand>("pages");
            AddPatch<ChangeNameOfPageCommand, ChangeNameOfPageApiCommand>("pages/{pageID}/changeName");
            AddPatch<ChangeIndexOfPageCommand, ChangeIndexOfPageApiCommand>("pages/changeIndex");

            // Signatures
            AddPost<CreateSignatureCommand, CreateSignatureApiCommand>("signatures");
            AddDelete<DeleteSignatureCommand, DeleteSignatureApiCommand>("signatures/{signatureID}");
            AddPatch<ChangeNameOfSignatureCommand, ChangeNameOfSignatureApiCommand>("signatures/{signatureID}/changeName");
            AddPatch<ChangeIndexOfSignatureCommand, ChangeIndexOfSignatureApiCommand>("signatures/{signatureID}/changeIndex");
        }
    }
}
