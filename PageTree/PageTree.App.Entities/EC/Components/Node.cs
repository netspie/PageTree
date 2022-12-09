using PageTree.Domain.EC;

namespace PageTree.Domain
{
    public class Node : ECComponent
    {
        public string ParentID { get; set; }
        public string ChildrenIDs { get; set; }
    }
}
