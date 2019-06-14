using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CFT.API.Repositories
{
    public class BusRepository : Repository<Buses>, IBusRepository
    {
        public BusRepository(DbContext context) : base(context)
        { 
        }
    }
}
