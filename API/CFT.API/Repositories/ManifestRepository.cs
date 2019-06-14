using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CFT.API.Repositories
{
    public class ManifestRepository : Repository<TripManifests>, IManifestRepository
    {
        public ManifestRepository(DbContext context) : base(context)
        {
        }
    }
}
