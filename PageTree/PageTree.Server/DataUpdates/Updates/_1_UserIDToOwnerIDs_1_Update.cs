using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using PageTree.Domain;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;

namespace PageTree.Server.DataUpdates
{
    public class UserIDToOwnerIDs_1_Update : BaseDataUpdate
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<ProjectUserList> _projectUserListRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Page> _pageListRepository;

        public UserIDToOwnerIDs_1_Update(
            IRepository<DataUpdate> dataUpdateRepository,
            IRepository<User> userRepository, 
            IRepository<ProjectUserList> projectUserListRepository,
            IRepository<Project> projectRepository, 
            IRepository<Page> pageListRepository) : base(dataUpdateRepository)
        {
            _userRepository = userRepository;
            _projectUserListRepository = projectUserListRepository;
            _projectRepository = projectRepository;
            _pageListRepository = pageListRepository;
        }

        protected override async Task<Result> PerformDataUpdate()
        {
            var res = Result.Success();

            var users = await _userRepository.GetAll(res);
            foreach (var user in users)
            {
                if (user.ProjectUserListID.IsNullOrEmpty())
                    continue;

                var projectUserList = await _projectUserListRepository.ModifyAndSaveEntity(
                    user.ProjectUserListID, e => e.OwnerID = user.ID, res);

                foreach (var projectID in projectUserList.ProjectsCreatedIDs)
                {
                    var project = await _projectRepository.ModifyAndSaveEntity(
                        projectID, e => e.OwnerID = user.ID, res);

                    var page = await _pageListRepository.ModifyAndSaveEntity(
                        project.RootPageID, e => e.OwnerID = user.ID, res);
                }
            }

            return res;
        }

        protected override string GetUpdateName() => "UserIDToOwnerIDs_1_Update";
    }
}
