using Common.Basic.DDD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemOwnerFunctions
    {
        public static void Rename<TItem>(string id, string newName, List<TItem> items, Action<TItem> renameItem)
            where TItem : Entity
        {
            if (string.IsNullOrEmpty(id))
                return;

            if (string.IsNullOrEmpty(newName))
                return;

            var item = items.FirstOrDefault(q => q.ID == id);
            if (item == null)
                return;

            renameItem(item);
        }
    }
}
