using Corelibs.Basic.Net;
using PageTree.Domain.Projects;

namespace PageTree.Server.ApiContracts
{
    public class EditProjectApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Project))]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public EditProjectApiCommand() { }

        public EditProjectApiCommand(string id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }
    }
}
