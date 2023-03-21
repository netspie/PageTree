using AutoMapper;
using Corelibs.BlazorShared;
using PageTree.App.Pages.Commands;
using PageTree.App.PageTemplates.Commands;
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
            AddPost<CreateSubPageCommand, CreateSubPageApiCommand>("pages/{pageID}/subpages");
            AddPost<CreateSubPagesFromTemplateCommand, CreateSubPagesFromTemplateApiCommand>("pages/{pageID}/subpages/createFromTemplate");
            AddPost<CreateLinkCommand, CreateLinkApiCommand>("pages/{pageID}/links");
            AddDelete<RemovePropertyCommand, RemovePropertyApiCommand>("pages");
            AddPatch<ChangeNameOfPageCommand, ChangeNameOfPageApiCommand>("pages/{pageID}/changeName");
            AddPatch<ChangeIndexOfPropertyCommand, ChangeIndexOfPropertyApiCommand>("pages/changeIndex");
            AddPatch<ChangeLevelOfPropertyCommand, ChangeLevelOfPropertyApiCommand>("pages/changeLevel");
            AddPatch<ChangeSignatureOfPageCommand, ChangeSignatureOfPageApiCommand>("pages/{pageID}/changeSignature");

            // Page Templates
            AddPost<CreateSubPageTemplateCommand, CreateSubPageTemplateApiCommand>("pageTemplates/{pageTemplateID}/subpages");
            AddDelete<RemovePropertyTemplateCommand, RemovePropertyTemplateApiCommand>("pageTemplates");
            AddPatch<ChangeNameOfPageTemplateCommand, ChangeNameOfPageTemplateApiCommand>("pageTemplates/{pageTemplateID}/changeName");
            AddPatch<ChangeNameOfPageTemplatePageCommand, ChangeNameOfPageTemplatePageApiCommand> ("pageTemplates/{pageTemplateID}/changePageName");
            AddPatch<ChangeIndexOfPropertyTemplateCommand, ChangeIndexOfPropertyTemplateApiCommand>("pageTemplates/changeIndex");
            AddPatch<ChangeLevelOfPropertyTemplateCommand, ChangeLevelOfPropertyTemplateApiCommand>("pageTemplates/changeLevel");
            AddPatch<ChangeSignatureOfPageTemplateCommand, ChangeSignatureOfPageTemplateApiCommand>("pageTemplates/{pageTemplateID}/changeSignature");
            AddPatch<ChangeExpandOfPageTemplateCommand, ChangeExpandOfPageTemplateApiCommand>("pageTemplates/{pageTemplateID}/changeExpand");

            // Signatures
            AddPost<CreateSignatureCommand, CreateSignatureApiCommand>("signatures");
            AddDelete<DeleteSignatureCommand, DeleteSignatureApiCommand>("signatures/{signatureID}");
            AddPatch<ChangeNameOfSignatureCommand, ChangeNameOfSignatureApiCommand>("signatures/{signatureID}/changeName");
            AddPatch<ChangeIndexOfSignatureCommand, ChangeIndexOfSignatureApiCommand>("signatures/{signatureID}/changeIndex");

            // Practice Categories
            AddPost<CreateSignatureCommand, CreateSignatureApiCommand>("practiceCategories");
            AddDelete<DeleteSignatureCommand, DeleteSignatureApiCommand>("practiceCategories/{practiceCategoryID}");
            AddPatch<ChangeNameOfSignatureCommand, ChangeNameOfSignatureApiCommand>("practiceCategories/{practiceCategoryID}/changeName");
            AddPatch<ChangeIndexOfSignatureCommand, ChangeIndexOfSignatureApiCommand>("practiceCategories/{practiceCategoryID}/changeIndex");
        }
    }
}
