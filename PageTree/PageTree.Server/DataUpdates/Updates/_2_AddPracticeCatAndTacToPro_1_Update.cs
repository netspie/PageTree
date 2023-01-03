using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using PageTree.Domain;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;

namespace PageTree.Server.DataUpdates
{
    public class _2_AddPracticeCatAndTacToPro_1_Update : BaseDataUpdate
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<ProjectUserList> _projectUserListRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Page> _pageListRepository;
        private readonly IRepository<PracticeCategory> _practiceCategoryRepository;
        private readonly IRepository<PracticeTactic> _practiceTacticRepository;

        public _2_AddPracticeCatAndTacToPro_1_Update(
            IRepository<DataUpdate> dataUpdateRepository,
            IRepository<User> userRepository,
            IRepository<ProjectUserList> projectUserListRepository,
            IRepository<Project> projectRepository,
            IRepository<Page> pageListRepository,
            IRepository<PracticeCategory> practiceCategoryRepository,
            IRepository<PracticeTactic> practiceTacticRepository) : base(dataUpdateRepository)
        {
            _userRepository = userRepository;
            _projectUserListRepository = projectUserListRepository;
            _projectRepository = projectRepository;
            _pageListRepository = pageListRepository;
            _practiceCategoryRepository = practiceCategoryRepository;
            _practiceTacticRepository = practiceTacticRepository;
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

                    if (project.PracticeCategoryRootID.IsNullOrEmpty() &&
                        project.PracticeTacticRootID.IsNullOrEmpty())
                    {
                        var practiceCategoryRoot = new PracticeCategory(NewID);
                        await _practiceCategoryRepository.Save(practiceCategoryRoot, res);
                        project.PracticeCategoryRootID = practiceCategoryRoot.ID;

                        var practiceTacticRoot = new PracticeTactic(NewID);
                        await _practiceTacticRepository.Save(practiceTacticRoot, res);
                        project.PracticeTacticRootID = practiceTacticRoot.ID;
                    }

                    await _projectRepository.Save(project, res);
                }
            }

            return res;
        }
    }
}
