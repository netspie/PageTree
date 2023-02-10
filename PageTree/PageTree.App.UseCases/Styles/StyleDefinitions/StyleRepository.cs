using Common.Basic.Blocks;
using Common.Basic.Repository;
using PageTree.App.Entities.Styles;

namespace PageTree.App.UseCases.Styles.StyleDefinitions
{
    public class StyleRepository : IRepository<Style>
    {
        public Task<Result> Clear()
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> ExistsOfName(string name, Func<Style, string> getName)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Style[]>> GetAll()
        {
            return Task.FromResult(Array.Empty<Style>().ToResult());
        }

        public Task<Result<Style[]>> GetAll(Action<int> setProgress, CancellationToken ct)
        {
            return Task.FromResult(Array.Empty<Style>().ToResult());
        }

        public Task<Result<Style>> GetBy(string id)
        {
            return Task.FromResult(Result<Style>.Failure());
        }

        public Task<Result<Style[]>> GetBy(IList<string> ids)
        {
            return Task.FromResult(Array.Empty<Style>().ToResult());
        }

        public Task<Result<Style>> GetOfName(string name, Func<Style, string> getName)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Save(Style item)
        {
            throw new NotImplementedException();
        }
    }
}
