using Microsoft.EntityFrameworkCore;

namespace QuickstartIdentityServer.Data.Services
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(IdentityServerContext dbContext) : base(dbContext)
        {

        }
    }
}