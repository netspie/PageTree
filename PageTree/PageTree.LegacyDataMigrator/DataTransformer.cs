using Common.Basic.Blocks;
using Common.Basic.Repository;
using PageTree.Domain;

namespace PageTree.LegacyDataMigrator;

public class DataTransformer
{
    public required string ProjectID { private get; set; }
    public required string OwnerID { private get; set; }
    public required string RootPageID { private get; set; }
    public required string LegacyRootPageID { private get; set; }
    public required string LegacyRootPageParentID { private get; set; }
    public required IRepository<Page> _pageRepository { private get; set; }

    public async Task<Result<PageTreeData>> Transform(LegacyData legacyData)
    {
        var result = Result<PageTreeData>.Success();

        var pagesDict = new Dictionary<string, Page>();
        var pages = await Task.WhenAll(legacyData.Pages.Select(async p =>
        {
            if (p.ID == LegacyRootPageParentID)
                return null;

            if (!pagesDict.TryGetValue(p.ID, out var page))
                page = new Page(p.ID);

            if (p.ID == LegacyRootPageID)
            {
                page = await _pageRepository.Get(RootPageID, result);
                var childrenIDs = page.ChildrenIDs.ToList();
                childrenIDs.ForEach(childID =>
                {
                    page.RemoveProperty(childID);
                });
            }

            page.Name = p.Name;
            page.OwnerID = OwnerID;
            page.ProjectID = ProjectID;
            page.SignatureID = p.TemplateSignatureID;
            page.ParentID = p.ParentID;
            if (p.ParentID == LegacyRootPageID)
            {
                page.ParentID = RootPageID;
            }
            if (p.ParentID == RootPageID)
            {
                page.ParentID = string.Empty;
            }

            foreach (var childID in p.OrderedPropertyIDs)
            {
                if (p.IsSubPage(childID))
                    page.CreateSubPage(childID, int.MaxValue);
                else
                {
                    page.CreateLink(childID, int.MaxValue);
                    if (!pagesDict.TryGetValue(childID, out var linkedPage))
                    {
                        linkedPage = new Page(childID);
                        pagesDict.Add(childID, linkedPage);
                    }

                    linkedPage.LinkedByIDs.Add(page.ID);
                }
            }

            if (!pagesDict.ContainsKey(page.ID))
                pagesDict.Add(page.ID, page);

            return page;
        }).ToArray());

        return Result<PageTreeData>.Success();
    }
}
