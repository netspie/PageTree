using Corelibs.Basic.Net;

namespace PageTree.Server.ApiContracts
{
    public class CreatePageApiCommand
    {
        [FromRoute]
        public string ID { get; set; }
        [FromRoute]
        public string ID2 { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }
}
