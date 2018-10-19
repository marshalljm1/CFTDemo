using System;
using System.Collections.Generic;
using System.Text;
using CFT.Promotions.Core.Interfaces;

namespace CFT.Promotions.Core.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDataStore<Trips> trips, 
                          IDataStore<TripTypes> tripTypes, 
                          IDataStore<TripManifests> manifests,
                          IDataStore<Messages> messages,
                          IDataStore<CctransInfo> cctransinfo)
        {
            Trips = trips;
            TripTypes = tripTypes;
            Manifests = manifests;
            Messages = messages;
            CCTransInfo = cctransinfo;

            Trips.ApiBase = "trips";
            TripTypes.ApiBase = "triptypes";
            Manifests.ApiBase = "manifest";
            Messages.ApiBase = "messages";
            CCTransInfo.ApiBase = "CCTransInfo";
        }

        public IDataStore<Trips> Trips { get; }
        public IDataStore<TripTypes> TripTypes { get; }
        public IDataStore<TripManifests> Manifests { get; }
        public IDataStore<Messages> Messages { get; }
        public IDataStore<CctransInfo> CCTransInfo { get; }
    }
}
