using BookApp.Context;
using BookApp.Entities;
using BookApp.Repository.Interfaces;

namespace BookApp.Repository.Implementations
{
    public class FlagRepository : BaseRepository<Flag>, IFlagRepository
    {
        public FlagRepository(BookAppContext context) : base(context)
        {
        }
    }
}
