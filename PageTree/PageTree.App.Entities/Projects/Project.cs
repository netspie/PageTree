using Common.Basic.DDD;

namespace PageTree.Domain.Projects
{
    public class Project : Entity
    {
        public string AuthorID { get; set; }
        public string RootPageID { get; set; }
    }

    public class ECProject : Entity
    {
        public string RootPageID { get; set; }
    }
}
