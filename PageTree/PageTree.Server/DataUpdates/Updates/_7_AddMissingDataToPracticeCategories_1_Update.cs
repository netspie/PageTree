using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Corelibs.Basic.Reflection;
using PageTree.Domain;
using PageTree.Domain.PageTemplates;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;

namespace PageTree.Server.DataUpdates
{
    public class _7_AddMissingDataToPracticeCategories_1_Update : BaseDataUpdate
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<ProjectUserList> _projectUserListRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<PracticeCategory> _practiceCategoryRepository;
        private readonly IRepository<PracticeTactic> _practiceTacticRepository;

        public _7_AddMissingDataToPracticeCategories_1_Update(
            IRepository<DataUpdate> dataUpdateRepository,
            IRepository<User> userRepository,
            IRepository<ProjectUserList> projectUserListRepository,
            IRepository<Project> projectRepository,
            IRepository<PracticeCategory> practiceCategoryRepository,
            IRepository<PracticeTactic> practiceTacticRepository) : base(dataUpdateRepository)
        {
            _userRepository = userRepository;
            _projectUserListRepository = projectUserListRepository;
            _projectRepository = projectRepository;
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
                    await Do(projectID);

                foreach (var projectID in projectUserList.ProjectsArchivedIDs)
                    await Do(projectID);

                async Task Do(string projectID)
                {
                    var project = await _projectRepository.Get(projectID, res);

                    PracticeCategory entity = null;
                    if (!project.PracticeCategoryRootID.IsNullOrEmpty())
                    {
                        entity = await _practiceCategoryRepository.Get(project.PracticeCategoryRootID, res);
                        if (entity.Name.IsNullOrEmpty())
                            entity.Name = PracticeCategory.GetRandomName();

                        entity.OwnerID = project.OwnerID;
                        entity.ProjectID = project.ID;
                        await _practiceCategoryRepository.Save(entity, res);
                    }
                    else
                    {
                        entity = new(NewID, PracticeCategory.GetRandomName(), project.OwnerID, projectID);
                        project.PracticeCategoryRootID = entity.ID;
                        await _practiceCategoryRepository.Save(entity, res);
                        await _projectRepository.Save(project, res);
                    }

                    PracticeTactic tactic = null;
                    if (!project.PracticeTacticRootID.IsNullOrEmpty())
                    {
                        tactic = await _practiceTacticRepository.Get(project.PracticeTacticRootID, res);
                        if (tactic.Name.IsNullOrEmpty())
                            tactic.Name = PracticeTactic.GetRandomName();

                        tactic.OwnerID = project.OwnerID;
                        tactic.ProjectID = project.ID;
                        await _practiceTacticRepository.Save(tactic, res);
                    }
                    else
                    {
                        tactic = new(NewID, PracticeTactic.GetRandomName(), project.OwnerID, projectID);
                        project.PracticeTacticRootID = tactic.ID;
                        await _practiceTacticRepository.Save(tactic, res);
                        await _projectRepository.Save(project, res);
                    }
                }
            }

            return res;
        }
    }
}
