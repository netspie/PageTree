using Common.Basic.Collections;
using System;
using System.Collections.Generic;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemFunctions
    {
        public static void ChangeType<TItem>(int index, List<TItem> items, Func<TItem, TItem> create)
        {
            if (!items.IsIndexInRange(index))
                return;

            var oldCriterium = items[index];
            items[index] = create(oldCriterium);
        }
    }
}
