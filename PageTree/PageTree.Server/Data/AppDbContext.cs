using Common.Basic.Blocks;
using Common.Basic.DDD;
using Common.Basic.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PageTree.Server.Data
{
    public class AppDbContext : DbContext
    {
        //public DbSet<Author> Authors { get; set; }
        //public DbSet<Author> Projects { get; set; }
    }

    public class User : Entity
    { 
    
    }

    public class UserRepository : IRepository<User>
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public UserRepository(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public Task<Result> Clear()
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> ExistsOfName(string name, Func<User, string> getName)
        {
            throw new NotImplementedException();
        }

        public Task<Result<User[]>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<User[]>> GetAll(Action<int> setProgress, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result<User>> GetBy(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<User[]>> GetBy(IList<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Result<User>> GetOfName(string name, Func<User, string> getName)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Save(User item)
        {
            throw new NotImplementedException();
        }
    }
}
