﻿using Common.Basic.Blocks;
using Common.Basic.Repository;
using Mediator;
using PageTree.App.UseCases.Common;
using PageTree.Domain.Practice;

namespace PageTree.App.UseCases.PracticeTactics.Commands;

public class ChangeNameOfPracticeTacticCommandHandler : BaseCommandHandler, ICommandHandler<ChangeNameOfPracticeTacticCommand, Result>
{
    private readonly IRepository<PracticeTactic> _repository;

    public ChangeNameOfPracticeTacticCommandHandler(
         IRepository<PracticeTactic> repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(ChangeNameOfPracticeTacticCommand command, CancellationToken ct)
    {
        var result = Result.Success();

        var item = await _repository.Get(command.PracticeTacticID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var parent = await _repository.Get(item.ParentID, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        var siblings = await _repository.Get(parent.ChildrenIDs, result);
        if (!result.ValidateSuccessAndValues())
            return result.Fail();

        if (siblings.Any(s => s.Name == command.Name))
            return result.Fail();

        if (!item.Rename(command.Name))
            return result.Fail();

        await _repository.Save(item, result);

        return result;
    }
}

public sealed record ChangeNameOfPracticeTacticCommand(string PracticeTacticID, string Name) : ICommand<Result>;
