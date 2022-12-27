using System.Collections.Generic;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemFunctions
    {
        public static void AddEmpty<TItem>(List<TItem> items)
            where TItem : new()
        {
            items.Add(new TItem());
        }
    }
}
