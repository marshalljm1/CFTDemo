using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CFT.API.Repositories
{
    public class TravelerRepository : Repository<Travelers>, ITravelerRepository
    {
        public TravelerRepository(DbContext context) : base(context)
        {
        }
    }
}
