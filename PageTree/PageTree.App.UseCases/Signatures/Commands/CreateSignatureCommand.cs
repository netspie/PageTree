using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.Entities.Signatures;
using PageTree.App.UseCases.Common;

namespace PageTree.App.UseCases.Signatures.Commands;

public class CreateSignatureCommandHandler : BaseCommandHandler, ICommandHandler<CreateSignatureCommand, Result>
{
    private readonly IRepository<Signature> _signatureRepository;

    public CreateSignatureCommandHandler(
         IRepository<Signature> signatureRepository)
    {
        _signatureRepository = signatureRepository;
    }

    public async ValueTask<Result> Handle(CreateSignatureCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var parentSignature = await _signatureRepository.Get(command.ParentID, result);
        if (!result.IsSuccess || parentSignature == null)
            return result.Fail();

        var signature = new Signature(NewID, $"Signature - {NewID.Take(8)}", parentSignature.OwnerID, parentSignature.ProjectID, command.ParentID);
        if (!parentSignature.CreateSignature(signature.ID, command.Index))
            return result.Fail();

        await _signatureRepository.Save(signature, result);
        await _signatureRepository.Save(parentSignature, result);

        return result;
    }
}

public sealed record CreateSignatureCommand(string ParentID, int Index) : ICommand<Result>;