using BookApp.Context;
using BookApp.Entities;
using BookApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookApp.Repository.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(BookAppContext context) : base(context)
    {
    }

    public async Task<User> GetUser(Expression<Func<User, bool>> expression)
    {
        return await _context.Users
            .Include(x => x.Role)
            .SingleOrDefaultAsync(expression);
    }
}
