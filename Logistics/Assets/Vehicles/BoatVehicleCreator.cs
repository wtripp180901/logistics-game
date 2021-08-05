using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatVehicleCreator : VehicleCreator {

    public BoatVehicleCreator(Map map) : base(map) { }

    protected override VEHICLE vehicleType
    {
        get
        {
            return VEHICLE.BOAT;
        }
    }

    protected override TransportHubFeature[] getAllRelevantHubs()
    {
        return HubObserver.getSeaports;
    }

    protected override List<Vector2> getPathOrNull(TransportHubFeature source, TransportHubFeature destination)
    {
        return new BoatRouteFinder().findPath(source, destination);
    }
}
