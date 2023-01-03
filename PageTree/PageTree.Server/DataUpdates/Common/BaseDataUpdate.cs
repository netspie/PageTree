using Common.Basic.Blocks;
using Common.Basic.Repository;

namespace PageTree.Server.DataUpdates
{
    public abstract class BaseDataUpdate : IDataUpdater
    {
        private readonly IRepository<DataUpdate> _dataUpdateRepository;

        public BaseDataUpdate(IRepository<DataUpdate> dataUpdateRepository)
        {
            _dataUpdateRepository = dataUpdateRepository;
        }

        public async Task<Result> Update()
        {
            var res = Result.Success();

            var updateName = GetUpdateName();
            var dataUpdate = await _dataUpdateRepository.Get(updateName, res);
            if (res.ValidateSuccessAndValues() && dataUpdate != null && dataUpdate.IsDone)
                return res;

            await PerformDataUpdate().AddTo(res);
            if (!res.IsSuccess)
                return res;

            dataUpdate = new DataUpdate(updateName, isDone: true);
            await _dataUpdateRepository.Save(dataUpdate, res);

            return res;
        }

        protected abstract Task<Result> PerformDataUpdate();
        protected virtual string GetUpdateName() => GetType().Name;

        protected static string NewID => Guid.NewGuid().ToString();
    }

    public static class DataUpdateExtensions
    {
        public static async Task<T> ModifyAndSaveEntity<T>(this IRepository<T> repository, string id, Action<T> modify, Result result)
        {
            var entity = await repository.Get(id, result);
            
            modify(entity);

            await repository.Save(entity, result);

            return entity;
        }
    }
}
