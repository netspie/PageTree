using Common.Basic.Collections;
using Common.Basic.Functional;

namespace Practicer.Domain.Pages.Common
{
    public static partial class EditableItemOwnerFunctions_NoName
    {
        public static bool Create<TItem>(string id, List<string> orderedItemsIDs, List<TItem> items, Func<TItem> create)
                where TItem : class
        {
            if (orderedItemsIDs.Contains(id))
                return false;

            if (!orderedItemsIDs.InsertOrAdd(id))
                return false;

            var item = create();
            if (item == null)
                return false;

            items.Add(item);
            return true;
        }

        public static bool Create<TItem>(string id, int index, List<string> orderedItemsIDs, List<TItem> items, Func<TItem> create)
            where TItem : class
        {
            if (orderedItemsIDs.Contains(id))
                return false;

            orderedItemsIDs.InsertClamped(id, index);
            
            var item = create();
            if (item == null)
                return false;

            items.Add(item);
            return true;
        }

        public static bool Create<TItem>(string id, string createAfterItemID, List<string> orderedItemsIDs, List<TItem> items, Func<TItem> create)
            where TItem : class
        {
            if (orderedItemsIDs.Contains(id))
                return false;

            if (!orderedItemsIDs.InsertOrAdd(id, createAfterItemID))
                return false;

            var item = create();
            if (item == null)
                return false;

            items.Add(item);
            return true;
        }

        public static bool Create<TItem>(string id, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : class
        {
            if (orderedItemsIDs.Contains(id))
                return false;

            if (!orderedItemsIDs.InsertOrAdd(id))
                return false;

            return CreateItemAndAdd(id, items);
        }

        public static bool Create<TItem>(string id, string createAfterItemID, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : class
        {
            if (orderedItemsIDs.Contains(id))
                return false;

            if (!orderedItemsIDs.InsertOrAdd(id, createAfterItemID))
                return false;

            return CreateItemAndAdd(id, items);
        }

        public static bool Create(this List<string> orderedItemsIDs, string id, int index = int.MaxValue)
        {
            if (orderedItemsIDs.Contains(id))
                return false;

            if (index < 0)
                return false;

            orderedItemsIDs.InsertClamped(id, index);
            return true;
        }

        public static bool Create<TItem>(int index, List<TItem> items, Func<TItem> createItem, out TItem item)
            where TItem : class
        {
            item = createItem();
            items.InsertClamped(item, index);

            return true;
        }

        public static bool Create<TItem>(string id, int index, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : class
        {
            if (orderedItemsIDs.Contains(id))
                return false;

            if (index < 0)
                return false;

            orderedItemsIDs.InsertClamped(id, index);
            return CreateItemAndAdd(id, items);
        }

        public static bool Create<TItem>(string id, string createAfterItemID, int index, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : class
        {
            if (Create(id, index, orderedItemsIDs, items))
                return true;

            return Create(id, createAfterItemID, orderedItemsIDs, items);
        }

        private static bool CreateItemAndAdd<TItem>(string id, List<TItem> items)
           where TItem : class
        {
            Activator.CreateInstance(typeof(TItem), new object[] { id })
                            .As<TItem>()
                            .Then(item => items.Add(item));

            return true;
        }
    }

    public static partial class EditableItemOwnerFunctions
    {
        public static void Create<TItem>(string id, string name, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : class
        {
            if (!orderedItemsIDs.InsertOrAdd(id))
                return;

            CreateItemAndAdd(id, name, items);
        }

        public static void Create<TItem>(string id, string name, string createAfterItemID, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : class
        {
            if (!orderedItemsIDs.InsertOrAdd(id, createAfterItemID))
                return;

            CreateItemAndAdd(id, name, items);
        }

        public static bool Create<TItem>(string id, string name, int index, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : class
        {
            if (index < 0)
                return false;

            orderedItemsIDs.InsertClamped(id, index);
            CreateItemAndAdd(id, name, items);

            return true;
        }

        public static void Create<TItem>(string id, string name, string createAfterItemID, int index, List<string> orderedItemsIDs, List<TItem> items)
            where TItem : class
        {
            if (Create(id, name, index, orderedItemsIDs, items))
                return;

            Create(id, name, createAfterItemID, orderedItemsIDs, items);
        }

        private static void CreateItemAndAdd<TItem>(string id, string name, List<TItem> items)
            where TItem : class
        {
            Activator.CreateInstance(typeof(TItem), new object[] { id, name })
                            .As<TItem>()
                            .Then(item => items.Add(item));
        }
    }
}
