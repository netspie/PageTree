using Corelibs.Basic.Net;
using PageTree.Domain.Practice;

namespace PageTree.Server.ApiContracts
{
    public class UpdateDataOfPracticeTacticApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(PracticeTactic))]
        public string PracticeTacticID { get; set; }
        public PracticeTacticItemApiVM Data { get; set; }
    }

    public class PracticeTacticItemApiVM
    {
        public PageItemIDsApiVM[] PageItems { get; set; }

        public string[] PagesToSkipIfContainsID { get; set; }
        public string[] PagesToSkipIfNotContainsID { get; set; }
    }

    public class PageItemIDsApiVM
    {
        public string[] PageSignatures { get; set; }
        public string[] QuestionsSignatures { get; set; }
        public string[] AnswersSignatures { get; set; }
    }
}
