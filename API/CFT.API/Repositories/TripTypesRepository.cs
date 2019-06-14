using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CFT.API.Repositories
{
    public class TripTypesRepository : Repository<TripTypes>, ITripTypesRepository
    {
        public TripTypesRepository(DbContext context) : base(context)
        {
        }
    }
}
