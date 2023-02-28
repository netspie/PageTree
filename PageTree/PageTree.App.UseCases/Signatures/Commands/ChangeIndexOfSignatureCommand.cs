using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Entities.Signatures;
using PageTree.App.UseCases.Common;

namespace PageTree.App.UseCases.Signatures.Commands;

public class ChangeIndexOfSignatureCommandHandler : BaseCommandHandler, ICommandHandler<ChangeIndexOfSignatureCommand, Result>
{
    private readonly IRepository<Signature> _signatureRepository;

    public ChangeIndexOfSignatureCommandHandler(
         IRepository<Signature> signatureRepository)
    {
        _signatureRepository = signatureRepository;
    }

    public async ValueTask<Result> Handle(ChangeIndexOfSignatureCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var signature = await _signatureRepository.Get(command.SignatureID, result);
        if (!result.ValidateSuccessAndValues() || signature == null)
            return result.Fail();

        var parentSignature = await _signatureRepository.Get(signature.ParentID, result);
        if (!result.IsSuccess || parentSignature == null)
            return result.Fail();

        if (!parentSignature.MoveSignature(command.SignatureID, command.Index))
            return result.Fail();

        await _signatureRepository.Save(parentSignature, result);

        return result;
    }
}

public sealed record ChangeIndexOfSignatureCommand(string SignatureID, int Index) : ICommand<Result>;
