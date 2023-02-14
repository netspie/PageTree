namespace PageTree.Client.Shared.Pages._PageTree
{
    public static class Uris
    {
        public const string Home = "/pageTree";
        public const string Learn = Home + "/learn";
        public const string Projects = Learn + "/projects";
        public const string Project = Projects + "/{projectID}";
        public const string ProjectEdit = Projects + "/{projectID}";
        public const string Pages = Project + "/pages";
        public const string Page = Pages + "/{pageID}";
    }
}
