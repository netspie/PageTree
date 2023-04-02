using Common.Basic.DDD;
using Corelibs.Basic.Architecture.DDD;

namespace PageTree.Domain.Projects
{
    public class Project : Entity, IOwnedEntity
    {
        public Project() {}
        public Project(
            string id, 
            string rootPageID,
            string rootPageTemplateID,
            string signatureRootID,
            string ownerID,
            string practiceCategoryRootID,
            string practiceTacticRootID) : base(id)
        {
            RootPageID = rootPageID;
            TemplatePageRootID = rootPageTemplateID;
            SignatureRootID = signatureRootID;
            OwnerID = ownerID;
            PracticeCategoryRootID = practiceCategoryRootID;
            PracticeTacticRootID = practiceTacticRootID;
        }

        public string RootPageID { get; set; } = new("");
        public string PublicRootPageID { get; set; } = new("");
        public string SignatureRootID { get; set; } = new("");
        public string TemplatePageRootID { get; set; } = new("");
        public string PracticeCategoryRootID { get; set; } = new("");
        public string PracticeTacticRootID { get; set; } = new("");
        public string Name { get; set; } = new("New Project");
        public string Description { get; set; } = new("");
        public string OwnerID { get; set; } = new("");
        public string StyleID { get; set; } = new("");
    }
}
