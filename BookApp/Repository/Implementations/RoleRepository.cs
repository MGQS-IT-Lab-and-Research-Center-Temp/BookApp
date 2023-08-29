using BookApp.Entities;
using BookApp.Repository.Interfaces;
using BookApp.Context;

namespace BookApp.Repository.Implementations
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(BookAppContext context) : base(context)
        {
        }
    }
}