using CFT.API.Interfaces;
using CFT.API.Models;

namespace CFT.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CFTContext _context;

        public UnitOfWork(CFTContext context)
        {
            _context = context;
            Trips = new TripsRepository(_context);
            Buses = new BusRepository(_context);
            Travelers = new TravelerRepository(_context);
            TripTypes = new TripTypesRepository(_context);
            Manifests = new ManifestRepository(_context);
            Messages = new MessagesRepository(_context);
        }
        
        public ITripsRepository Trips { get; }
        public IBusRepository Buses { get; }
        public ITravelerRepository Travelers { get; }
        public ITripTypesRepository TripTypes { get; }
        public IManifestRepository Manifests { get; }
        public IMessageRepository Messages { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
