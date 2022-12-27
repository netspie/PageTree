using Common.Basic.Collections;
using System.Collections.Generic;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemFunctions
    {
        public static void Remove<TItem>(int index, List<TItem> items)
        {
            if (!items.IsIndexInRange(index))
                return;

            items.RemoveAt(index);
        }
    }
}
