using Common.Basic.DDD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemOwnerFunctions
    {
        public static void SubItem_ChangeType<TItem>(string id, List<TItem> items, Action<TItem> changeType)
            where TItem : Entity
        {
            var item = items.FirstOrDefault(q => q.ID == id);
            if (item == null)
                return;

            changeType(item);
        }

    }
}
