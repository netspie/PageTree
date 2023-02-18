using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Entities.Signatures;
using PageTree.App.UseCases.Common;

namespace PageTree.App.UseCases.Signatures.Commands;

public class DeleteSignatureCommandHandler : BaseCommandHandler, ICommandHandler<DeleteSignatureCommand, Result>
{
    private readonly IRepository<Signature> _signatureRepository;

    public DeleteSignatureCommandHandler(
         IRepository<Signature> signatureRepository)
    {
        _signatureRepository = signatureRepository;
    }

    public async ValueTask<Result> Handle(DeleteSignatureCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var signature = await _signatureRepository.Get(command.SignatureID, result);
        if (!result.IsSuccess || signature == null)
            return result.Fail();

        var parentSignature = await _signatureRepository.Get(signature.ParentID, result);
        if (!parentSignature.RemoveSignature(signature.ID))
            return result.Fail();

        await _signatureRepository.Delete(signature.ID);
        await _signatureRepository.Save(parentSignature, result);

        return result;
    }
}

public sealed record DeleteSignatureCommand(string SignatureID) : ICommand<Result>;
