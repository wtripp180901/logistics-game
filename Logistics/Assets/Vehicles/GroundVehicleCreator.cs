using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundVehicleCreator : VehicleCreator {

    public GroundVehicleCreator(Map map) : base(map) { }

    protected override VEHICLE vehicleType { get { return VEHICLE.VAN; } }

    protected override List<Vector2> getPathOrNull(TransportHubFeature source, TransportHubFeature destination)
    {
        return new GroundRouteFinder().findPath(source, destination);
    }

    protected override TransportHubFeature[] getAllRelevantHubs()
    {
        return HubObserver.getHubs;
    }
}
