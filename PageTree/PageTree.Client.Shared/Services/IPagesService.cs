using Common.Basic.CQRS.Command;
using Common.Basic.CQRS.Query;
using Mediator;
using PageTree.App.Pages.Queries;
using PageTree.App.Practice.Queries;

namespace PageTree.Client.Shared.Services
{
    public interface IPagesService
    {
        Task<PageVM> GetPage(string id);
        Task<PracticeCardDTO[]> GetPracticeCards(string pageID, string practiceTacticID);
    }

    //public sealed class HttpClientRepo
    //{
    //    Task<PageVM> GetPage(string id);
    //    Task<PracticeCardDTO[]> GetPracticeCards(string pageID, string practiceTacticID);
    //}

    public sealed class PagesService : IPagesService
    {
        private IMediator _mediator;

        public async Task<PageVM> GetPage(string id)
        {
            var result = await _mediator.Send(new GetPageOfIDQuery(id));
            
            return result.Get().PageVM;
        }

        public Task<PracticeCardDTO[]> GetPracticeCards(string pageID, string practiceTacticID)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class PagesServiceDecorator : IPagesService
    {
        protected readonly IPagesService _decorated;

        public PagesServiceDecorator(IPagesService decorated)
        {
            _decorated = decorated;
        }

        public virtual Task<PageVM> GetPage(string id) => _decorated.GetPage(id);
        public virtual Task<PracticeCardDTO[]> GetPracticeCards(string pageID, string practiceTacticID) => _decorated.GetPracticeCards(pageID, practiceTacticID);
    }

    public sealed class ServiceRedirectDecorator : PagesServiceDecorator
    {
        public ServiceRedirectDecorator(IPagesService decorated) : base(decorated) { }

        public override async Task<PageVM> GetPage(string id)
        {
            PageVM vm = null;
            try
            {
                vm = await _decorated.GetPage(id);
            }
            catch (Exception ex) // AccessTokenNotAvailableException
            {
                // ex.Redirect();
                return null!;
            }

            return vm!;
        }

        public override Task<PracticeCardDTO[]> GetPracticeCards(string pageID, string practiceTacticID)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class ClientCode
    {
        public void Setup()
        {
            var pagesService = new PagesService();
            var pagesServiceWithRedirect = new ServiceRedirectDecorator(pagesService);

            IQueryExecutor queryExecutor = null;
            ICommandExecutor commandExecutor = null;

        }
    }
}
