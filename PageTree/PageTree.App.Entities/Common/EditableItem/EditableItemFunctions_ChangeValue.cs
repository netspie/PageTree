using Common.Basic.Collections;
using System;
using System.Collections.Generic;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemFunctions
    {
        public static void ChangeValue<TItem>(int index, string value, List<TItem> items, Func<TItem, bool> canChangeValue, Func<TItem, TItem> create)
        {
            if (!items.IsIndexInRange(index))
                return;

            if (value.IsNullOrEmpty())
                return;

            var oldValueHolder = items[index];
            if (!canChangeValue(oldValueHolder))
                return;

            items[index] = create(oldValueHolder);
        }
    }
}
