using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using PageTree.Domain;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;

namespace PageTree.Server.DataUpdates
{
    public class _3_AddProjectIDToPage_1_Update : BaseDataUpdate
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<ProjectUserList> _projectUserListRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Page> _pageRepository;

        public _3_AddProjectIDToPage_1_Update(
            IRepository<DataUpdate> dataUpdateRepository,
            IRepository<User> userRepository,
            IRepository<ProjectUserList> projectUserListRepository,
            IRepository<Project> projectRepository,
            IRepository<Page> pageListRepository) : base(dataUpdateRepository)
        {
            _userRepository = userRepository;
            _projectUserListRepository = projectUserListRepository;
            _projectRepository = projectRepository;
            _pageRepository = pageListRepository;
        }

        protected override async Task<Result> PerformDataUpdate()
        {
            var res = Result.Success();

            var users = await _userRepository.GetAll(res);
            foreach (var user in users)
            {
                if (user.ProjectUserListID.IsNullOrEmpty())
                    continue;

                var projectUserList = await _projectUserListRepository.Get(
                    user.ProjectUserListID, res);

                foreach (var projectID in projectUserList.ProjectsCreatedIDs)
                {
                    var project = await _projectRepository.Get(projectID, res);
                    var rootPage = await _pageRepository.Get(project.RootPageID, res);
                    rootPage.ProjectID = projectID;
                    await _pageRepository.Save(rootPage, res);

                    await SetProjectIds(rootPage.SubPages, projectID);
                }
            }

            return res;

            async Task SetProjectIds(IList<string> ids, string projectID)
            {
                foreach (var id in ids)
                {
                    var page = await _pageRepository.Get(id, res);
                    page.ProjectID = projectID;
                    await _pageRepository.Save(page, res);

                    await SetProjectIds(page.SubPages, projectID);
                }
            }
        }
    }
}
