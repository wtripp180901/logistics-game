using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneVehicleCreator : VehicleCreator
{

    public PlaneVehicleCreator(Map map) : base(map) { }

    protected override VEHICLE vehicleType
    {
        get
        {
            Debug.Log("implement this shit");
            return VEHICLE.PLANE;
        }
    }

    protected override List<Vector2> getPathOrNull(TransportHubFeature source, TransportHubFeature destination)
    {
        if (source.featureType == FEATURES.AIRPORT && destination.featureType == FEATURES.AIRPORT)
        {
            return new List<Vector2>() { source.parent.position, destination.parent.position };
        }
        else return null;
    }

    protected override TransportHubFeature[] getAllRelevantHubs()
    {
        return HubObserver.getAirports;
    }
}
