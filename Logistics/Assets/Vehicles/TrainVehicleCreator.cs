using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainVehicleCreator : VehicleCreator {

    public TrainVehicleCreator(Map map) : base(map) { }

    protected override TransportHubFeature[] getAllRelevantHubs()
    {
        return HubObserver.getRailyards;
    }

    protected override List<Vector2> getPathOrNull(TransportHubFeature source, TransportHubFeature destination)
    {
        return RouteFinder.findPath(source, destination);
    }
}
