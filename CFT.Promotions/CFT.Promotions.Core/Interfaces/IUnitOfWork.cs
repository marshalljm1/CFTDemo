using System;
using System.Collections.Generic;
using System.Text;
using CFT.Promotions.Core.Models;

namespace CFT.Promotions.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IDataStore<Trips> Trips { get; }
        IDataStore<TripTypes> TripTypes { get; }
        IDataStore<TripManifests> Manifests { get; }
        IDataStore<Messages> Messages { get; }
        IDataStore<CctransInfo> CCTransInfo { get; }
    }
}
