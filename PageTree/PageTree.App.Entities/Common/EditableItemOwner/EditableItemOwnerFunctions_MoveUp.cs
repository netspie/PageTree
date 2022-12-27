using Common.Basic.Collections;
using System.Collections.Generic;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemOwnerFunctions
    {
        public static bool MoveUp(string id, List<string> orderedItemsIDs)
        {
            var index = orderedItemsIDs.IndexOf(id);
            if (index == -1)
                return false;

            return Reorder(id, index - 1, orderedItemsIDs);
        }

        public static bool MoveUp<T>(string id, List<string> orderedItemsIDs, List<T> orderedItems2)
        {
            var index = orderedItemsIDs.IndexOf(id);
            if (index == -1)
                return false;

            if (!Reorder(id, index - 1, orderedItemsIDs))
                return false;

            orderedItems2.Swap(index, index - 1);

            return true;
        }
    }
}
