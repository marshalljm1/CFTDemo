using CFT.App.Core.Utility;
using CFT.Data.Models;

namespace CFT.App.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IDataStore<Trips> Trips { get; }
        IDataStore<TripTypes> TripTypes { get; }
        IDataStore<TripManifests> Manifests { get; }
        IDataStore<Messages> Messages { get; }
    }
}
