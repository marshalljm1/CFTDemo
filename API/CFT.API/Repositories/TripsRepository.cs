using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CFT.API.Repositories
{
    public class TripsRepository : Repository<Trips>, ITripsRepository
    {
        public TripsRepository(DbContext context) : base(context)
        {
        }
    }
}
