using System;
using System.Collections.Generic;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemOwnerFunctions
    {
        public static void Move(string id, List<string> orderedItemsIDs)
        {
            var index = orderedItemsIDs.IndexOf(id);
            if (index == -1)
                return;

            Reorder(id, index + 1, orderedItemsIDs);
        }

        public static bool Move(
            string id, 
            int index, 
            bool asType1,
            List<string> orderedItemsIDs,
            Func<string, int, bool> reorder,
            Func<string, int, bool> createType1,
            Func<string, int, bool> createType2)
        {
            if (orderedItemsIDs.Contains(id))
                return reorder(id, index);

            if (asType1)
                return createType1(id, index);

            return createType2(id, index);
        }
    }
}
