using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class UpdateDataOfPracticeTacticApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PracticeTactic))]
        public string PracticeTacticID { get; set; }
        public PageItemIDsApiVM Data { get; set; }
    }

    public class PageItemIDsApiVM
    {
        public string[] PageSignatures { get; set; }
        public string[] QuestionsSignatures { get; set; }
        public string[] AnswersSignatures { get; set; }
    }
}
