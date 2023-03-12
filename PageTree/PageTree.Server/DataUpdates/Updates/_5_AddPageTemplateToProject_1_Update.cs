using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using PageTree.App.Entities.Signatures;
using PageTree.Domain.PageTemplates;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;

namespace PageTree.Server.DataUpdates
{
    public class _5_AddPageTemplateToProject_1_Update : BaseDataUpdate
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<ProjectUserList> _projectUserListRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<PageTemplate> _pageTemplateRepository;

        public _5_AddPageTemplateToProject_1_Update(
            IRepository<DataUpdate> dataUpdateRepository,
            IRepository<User> userRepository,
            IRepository<ProjectUserList> projectUserListRepository,
            IRepository<Project> projectRepository,
            IRepository<PageTemplate> pageTemplateRepository) : base(dataUpdateRepository)
        {
            _userRepository = userRepository;
            _projectUserListRepository = projectUserListRepository;
            _projectRepository = projectRepository;
            _pageTemplateRepository = pageTemplateRepository;
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

                    PageTemplate pageTemplate = null;
                    if (!project.TemplatePageRootID.IsNullOrEmpty())
                    {
                        pageTemplate = await _pageTemplateRepository.Get(project.TemplatePageRootID, res);
                        pageTemplate.OwnerID = project.OwnerID;
                        await _pageTemplateRepository.Save(pageTemplate, res);
                    }
                    else
                    {
                        pageTemplate = new PageTemplate(NewID, "Root Page Template", "", project.OwnerID, projectID);
                        project.TemplatePageRootID = pageTemplate.ID;
                        await _pageTemplateRepository.Save(pageTemplate, res);
                        await _projectRepository.Save(project, res);
                    }

                }
            }

            return res;
        }
    }
}
