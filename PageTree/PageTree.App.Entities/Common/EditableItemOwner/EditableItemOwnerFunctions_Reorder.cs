using Common.Basic.Collections;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemOwnerFunctions
    {
        public static bool Reorder(string id, int desiredIndex, List<string> orderedItemsIDs)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            if (!orderedItemsIDs.IsIndexInRange(desiredIndex))
                return false;

            var index = orderedItemsIDs.IndexOf(id);
            if (index == -1)
                return false;

            if (index == desiredIndex)
                return false;

            orderedItemsIDs.Swap(index, desiredIndex);

            return true;
        }
    }
}
