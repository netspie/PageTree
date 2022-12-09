using Common.Basic.Blocks;
using Mediator;

namespace PageTree.Client.Shared.CQRS
{
    public interface IMCommandExecutor
    {
        Task<Result<TResponse>> Execute<TResponse>(ICommand<Result<TResponse>> command, CancellationToken cancellationToken = default);

        Task<TResponse> ExecuteForValue<TResponse>(ICommand<Result<TResponse>> command, CancellationToken cancellationToken = default);

        Task Execute<TResponse>(ICommand<Result> command, CancellationToken cancellationToken = default);
    }
}
