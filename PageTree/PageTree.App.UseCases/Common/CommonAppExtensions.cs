using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Common.Basic.Threading;
using PageTree.App.UseCases.Common;
using PageTree.Domain;

namespace PageTree.App.Common;

public static class CommonExtensions
{
    public async static Task<IdentityVM[]> ToIdentityVM<T>(
        this IEnumerable<string> ids, IRepository<T> repository, Func<T, string> getName, Result result)
    {
        return await ids.SelectOrDefault(async id =>
        {
            var entity = await repository.Get(id, result);
            var name = entity is not null ? getName(entity) : "---";
            return new IdentityVM(id, name);
        }).Values();
    }

    public static async Task<Result> GetParents<T>(
        this IRepository<T> pageRepository, T page, Func<T, string> getParentID, List<T> parents)
    {
        var result = Result.Success();

        var parentID = getParentID(page);
        if (string.IsNullOrEmpty(parentID))
            return result;

        var parentEntity = await pageRepository.Get(parentID, result);
        parents.Add(parentEntity);
        await GetParents(pageRepository, parentEntity, getParentID, parents);

        return result;
    }

    public static bool IsOfParentOfName(this Page page, IRepository<Page> pageRepo, string name)
    {
        if (page.ParentID.IsNullOrEmpty())
            return false;

        var parentPage = pageRepo.GetBy(page.ParentID).Value();
        if (parentPage == null)
            return false;

        if (parentPage.Name == name)
            return true;

        return IsOfParentOfName(parentPage, pageRepo, name);
    }

    public static bool IsOfParentOfID(this Page page, IRepository<Page> pageRepo, string id)
    {
        if (page.ParentID.IsNullOrEmpty())
            return false;

        var parentPage = pageRepo.GetBy(page.ParentID).Value();
        if (parentPage == null)
            return false;

        if (parentPage.ID == id)
            return true;

        return IsOfParentOfID(parentPage, pageRepo, id);
    }

    public static Page GetChildOfName(this Page page, IRepository<Page> repo, string name)
    {
        if (!page || page.ChildrenIDs == null)
            return null;

        foreach (var id in page.ChildrenIDs)
        {
            var childPage = repo.GetBy(id).Value();
            if (childPage.Name == name)
                return childPage;
        }

        return null;
    }

    public static Page GetNestedChildOfID(this Page page, IRepository<Page> repo, string searchedID)
    {
        if (!page || page.ChildrenIDs == null)
            return null;

        foreach (var id in page.ChildrenIDs)
        {
            var childPage = repo.GetBy(id).Value();
            if (id == searchedID)
                return childPage;

            if (!page.IsSubPage(childPage.ID))
                continue;

            var res = GetNestedChildOfID(childPage, repo, searchedID);
            if (res)
                return res;
        }

        return null;
    }

    public static bool ContainsNestedChildOfID(this Page page, IRepository<Page> repo, string searchedID) =>
        page.GetNestedChildOfID(repo, searchedID) != null;

    public static bool ContainsAnyNestedChildOfID(this Page page, IRepository<Page> repo, IEnumerable<string> searchedIDs)
    {
        foreach (var id in searchedIDs)
            if (page.ContainsNestedChildOfID(repo, id))
                return true;

        return false;
    }

    public static bool ContainsAll(this Page page, IRepository<Page> repo, IEnumerable<string> searchedIDs)
    {
        foreach (var id in searchedIDs)
            if (!page.ContainsNestedChildOfID(repo, id))
                return false;

        return true;
    }

    public static Page GetNestedChildOfName(this Page page, IRepository<Page> repo, string name)
    {
        if (!page || page.ChildrenIDs == null)
            return null;

        foreach (var id in page.ChildrenIDs)
        {
            var childPage = repo.GetBy(id).Value();
            if (childPage.Name == name)
                return childPage;

            var res = GetNestedChildOfName(childPage, repo, name);
            if (res)
                return res;
        }

        return null;
    }
}
