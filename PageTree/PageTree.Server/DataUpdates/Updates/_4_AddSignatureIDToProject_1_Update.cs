using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using Corelibs.Basic.Collections;
using PageTree.App.Entities.Signatures;
using PageTree.Domain;
using PageTree.Domain.Practice;
using PageTree.Domain.Projects;
using PageTree.Domain.Users;

namespace PageTree.Server.DataUpdates
{
    public class _4_AddSignatureIDToProject_1_Update : BaseDataUpdate
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<ProjectUserList> _projectUserListRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Signature> _signatureRepository;

        public _4_AddSignatureIDToProject_1_Update(
            IRepository<DataUpdate> dataUpdateRepository,
            IRepository<User> userRepository,
            IRepository<ProjectUserList> projectUserListRepository,
            IRepository<Project> projectRepository,
            IRepository<Signature> signatureRepository) : base(dataUpdateRepository)
        {
            _userRepository = userRepository;
            _projectUserListRepository = projectUserListRepository;
            _projectRepository = projectRepository;
            _signatureRepository = signatureRepository;
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
                    if (!project.SignatureRootID.IsNullOrEmpty())
                        continue;

                    var signatureRoot = new Signature(NewID, "Root Signature", project.OwnerID, projectID);
                    project.SignatureRootID = signatureRoot.ID;

                    await _signatureRepository.Save(signatureRoot, res);
                    await _projectRepository.Save(project, res);
                }
            }

            return res;
        }
    }
}
