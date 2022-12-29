using Common.Basic.DDD;

namespace PageTree.Server.DataUpdates
{
    public class DataUpdate : Entity
    {
        public DataUpdate() { }
        public DataUpdate(string id, bool isDone) : base(id)
        {
            IsDone = isDone;
        }

        public string Description { get; set; } = new("");
        public bool IsDone { get; set; }
    }
}
