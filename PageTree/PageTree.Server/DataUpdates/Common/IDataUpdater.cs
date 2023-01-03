using Common.Basic.Blocks;

namespace PageTree.Server.DataUpdates
{
    public interface IDataUpdater
    {
        Task<Result> Update();
    }
}
