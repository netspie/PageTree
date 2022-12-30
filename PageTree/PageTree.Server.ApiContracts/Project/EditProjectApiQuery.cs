using PageTree.Server.ApiContracts.Attributes;

namespace PageTree.Server.ApiContracts.Project
{
    public class EditProjectApiCommand
    {
        [FromRoute]
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
