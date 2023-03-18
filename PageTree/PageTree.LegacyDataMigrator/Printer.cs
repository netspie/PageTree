using Common.Basic.Blocks;
using Common.Basic.DDD;

namespace PageTree.LegacyDataMigrator;

public class Printer
{
    public void Print(Entity entity, Result result)
    {
        if (result.IsSuccess)
            Console.WriteLine("");
    }
}
