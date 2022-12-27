using Common.Basic.DDD;

namespace Domain.Common
{
    public class ListEntity : Entity
    {
        public List<string> OrderedItemsIDs { get; private set; }

        public ListEntity(
            string id,
            List<string> orderedItemsIDs)
            : base(id)
        {
            OrderedItemsIDs = orderedItemsIDs ?? new List<string>();
        }

        public ListEntity(string id)
            : base(id)
        {
            OrderedItemsIDs = new List<string>();
        }
    }
}
