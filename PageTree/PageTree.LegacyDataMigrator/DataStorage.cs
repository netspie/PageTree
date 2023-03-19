using Common.Basic.Blocks;
using Common.Basic.Repository;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;

namespace PageTree.LegacyDataMigrator;

public class DataStorage
{
    public required IRepository<Page> _pageRepository { private get; set; }
    public required IRepository<Signature> _signatureRepository { private get; set; }
    public required Data.AppDbContext _dbContext { private get; set; }
    public required string RootPageID { private get; set; }

    public async Task<Result> Store(PageTreeData data)
    {
        var result = Result.Success();

        //foreach (var signature in data.Signatures)
        //{
        //    if (!signature)
        //        continue;

        //    await _signatureRepository.Delete(signature.ID);
        //}

        //foreach (var page in data.Pages)
        //{
        //    if (!page)
        //        continue;

        //    if (page.ID == RootPageID)
        //        continue;

        //    await _pageRepository.Delete(page.ID);
        //}

        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {

                int i = 0;
                foreach (var signature in data.Signatures)
                {
                    if (!signature)
                        continue;

                    Console.WriteLine($"{i} / {data.Signatures.Length}");
                    var saveResult = await _signatureRepository.Save(signature);
                    result += saveResult;
                    if (!saveResult.IsSuccess)
                    {
                        Console.WriteLine($"{signature} - could not save");
                        return result;
                    }

                    i++;
                }

                i = 0;
                foreach (var page in data.Pages)
                {
                    if (!page)
                        continue;

                    Console.WriteLine($"{i} / {data.Pages.Length}");

                    var saveResult = await _pageRepository.Save(page);
                    result += saveResult;
                    if (!saveResult.IsSuccess)
                    {
                        Console.WriteLine($"{page} - could not save");
                        return result;
                    }

                    i++;
                }

                if (result.IsSuccess)
                    await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.ToString());
                return result.Fail();
            }
        }


        return Result.Failure();
    }
}
