using System;

namespace CFT.API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITripsRepository Trips { get; }
        IBusRepository Buses { get; }
        ITravelerRepository Travelers { get; }
        ITripTypesRepository TripTypes { get; }
        IManifestRepository Manifests { get; }
        IMessageRepository Messages { get; }
    
        int Complete();
    }
}
