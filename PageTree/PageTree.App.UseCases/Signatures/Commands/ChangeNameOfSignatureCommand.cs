using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Entities.Signatures;
using PageTree.App.UseCases.Common;

namespace PageTree.App.UseCases.Signatures.Commands;

public class ChangeNameOfSignatureCommandHandler : BaseCommandHandler, ICommandHandler<ChangeNameOfSignatureCommand, Result>
{
    private readonly IRepository<Signature> _signatureRepository;

    public ChangeNameOfSignatureCommandHandler(
         IRepository<Signature> signatureRepository)
    {
        _signatureRepository = signatureRepository;
    }

    public async ValueTask<Result> Handle(ChangeNameOfSignatureCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var signature = await _signatureRepository.Get(command.SignatureID, result);
        if (!result.IsSuccess || signature == null)
            return result.Fail();

        var parentSignature = await _signatureRepository.Get(signature.ParentID, result);
        if (!result.IsSuccess || parentSignature == null)
            return result.Fail();

        var siblingSignatures = await _signatureRepository.Get(parentSignature.ChildrenIDs, result);
        if (!result.IsSuccess || siblingSignatures == null)
            return result.Fail();

        if (siblingSignatures.Any(s => s.Name == command.Name))
            return result.Fail();

        if (!signature.Rename(command.Name))
            return result.Fail();

        await _signatureRepository.Save(signature, result);

        return result;
    }
}

public sealed record ChangeNameOfSignatureCommand(string SignatureID, string Name) : ICommand<Result>;
