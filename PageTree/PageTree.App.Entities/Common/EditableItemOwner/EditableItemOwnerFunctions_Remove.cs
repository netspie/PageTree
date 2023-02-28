using Common.Basic.DDD;
using System.Collections.Generic;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemOwnerFunctions
    {
        public static bool Remove<TItem>(string id, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : Entity
        {
            if (string.IsNullOrEmpty(id))
                return false;

            if (!orderedItemsIDs.Contains(id))
                return false;

            if (!items.Exists(item => item.ID == id))
                return false;

            items.RemoveAll(item => item.ID == id);
            orderedItemsIDs.RemoveAll(orderedItemID => orderedItemID == id);
            
            return true;
        }

        public static bool Remove(string id, List<string> orderedItemsIDs, List<string> items)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            if (!orderedItemsIDs.Contains(id))
                return false;

            if (!items.Exists(itemID => itemID == id))
                return false;

            items.RemoveAll(itemID => itemID == id);
            orderedItemsIDs.RemoveAll(orderedItemID => orderedItemID == id);

            return true;
        }

        public static bool Remove<TItem, TAdditionalItem>(string id, List<string> orderedItemsIDs, List<TAdditionalItem> otherItems, List<TItem> items)
            where TItem : Entity
        {
            if (!Remove(id, orderedItemsIDs, items))
                return false;

            var index = orderedItemsIDs.IndexOf(id);
            otherItems.RemoveAt(index);

            return true;
        }

        public static bool Remove(string id, List<string> orderedItemsIDs)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            if (!orderedItemsIDs.Contains(id))
                return false;

            orderedItemsIDs.RemoveAll(orderedItemID => orderedItemID == id);

            return true;
        }
    }
}
