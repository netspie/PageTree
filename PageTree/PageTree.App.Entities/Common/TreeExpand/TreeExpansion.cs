namespace BlazorComponentsLayersTest.Entities
{
    public class TreeExpansion
    {
        public string ID { get; private set; }
        public TreeNode RootNode { get; private set; }

        protected TreeExpansion() {}
        public TreeExpansion(string id)
        {
            ID = id;
            RootNode = new TreeNode(id);
        }

        public bool Expand(string id) => FindAndPerform(id, n => n.IsExpanded = true);
        public bool Collapse(string id) => FindAndPerform(id, n => n.IsExpanded = false);
        public bool SwitchExpand(string id) => FindAndPerform(id, n => n.IsExpanded = !n.IsExpanded);
        public void Reset()
        {
            RootNode = new TreeNode(ID);
        }

        private bool FindAndPerform(string id, Action<TreeNode> action)
        {
            var node = RootNode.FindNode(id);
            if (node == null)
                return false;

            action(node);
            return true;
        }
    }

    public class TreeNode
    {
        public string ID { get; private set; } = new("");
        public List<TreeNode> Children { get; private set; } = new();
        public bool IsExpanded { get; set; }

        public TreeNode(string id)
        {
            ID = id;
        }

        public TreeNode FindNode(string id)
        {
            if (id == ID)
                return this;

            foreach (var child in Children)
            {
                var node = child.FindNode(id);
                if (node != null)
                    return node;
            }

            return null;
        }
    }

    public class PageExpansion : TreeExpansion
    {

    }

    public class SignatureConfiguration : TreeExpansion
    {

    }

    public class PageTemplateConfiguration : TreeExpansion
    {

    }
}
