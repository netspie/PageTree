using System;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemFunctions
    {
        public static bool Rename(string newName, Action rename)
        {
            if (string.IsNullOrEmpty(newName))
                return false;

            rename();

            return true;
        }

        public static bool Rename_AllowEmpty(string newName, Action<string> rename)
        {
            if (string.IsNullOrEmpty(newName))
                newName = string.Empty;

            rename(newName);

            return true;
        }
    }
}
