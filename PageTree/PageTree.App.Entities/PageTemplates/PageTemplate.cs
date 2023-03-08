using PageTree.Domain;
using Practicer.Domain.Pages.Common;

namespace PageTree.App.Entities.PageTemplates
{
    public class PageTemplate : Page
    {
        public PageTemplate() {}
        public PageTemplate(string id, string templateName) : base(id) => TemplateName = templateName;
        public PageTemplate(string id, string templateName, string pageName) : this(id, templateName) { Name = pageName; }
        public PageTemplate(string id, string templateName, string pageName, string ownerID) : this(id, pageName, templateName) { OwnerID = ownerID; }
        public PageTemplate(string id, string templateName, string pageName, string ownerID, string projectID) : this(id, pageName, ownerID, templateName)
            { ProjectID = projectID; }

        public string TemplateName { get; private set; } = "New Template";

        public bool RenameTemplate(string newName) =>
          EditableItemFunctions.Rename(newName, () => TemplateName = newName);
    }
}
