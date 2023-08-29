using BookApp.Entities;
using System.Linq.Expressions;

namespace BookApp.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(Expression<Func<User, bool>> expression);
    }
}
