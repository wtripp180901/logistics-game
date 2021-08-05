using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HubObserver {

    private static List<TransportHubFeature> hubs = new List<TransportHubFeature>(); public static TransportHubFeature[] getHubs { get { return hubs.ToArray(); } }
    private static List<TransportHubFeature> airports = new List<TransportHubFeature>(); public static TransportHubFeature[] getAirports { get { return airports.ToArray(); } }
    private static List<TransportHubFeature> railyards = new List<TransportHubFeature>(); public static TransportHubFeature[] getRailyards { get { return railyards.ToArray(); } }
    private static List<TransportHubFeature> seaports = new List<TransportHubFeature>(); public static TransportHubFeature[] getSeaports { get { return seaports.ToArray(); } }

    public static void subscribe(TransportHubFeature hub)
    {
        hubs.Add(hub);
        if (hub.featureType == FEATURES.AIRPORT) airports.Add(hub);
        if (hub.featureType == FEATURES.RAILYARD) railyards.Add(hub);
        if (hub.featureType == FEATURES.SEAPORT) seaports.Add(hub);
    }

    public static void unsubscribe(TransportHubFeature hub)
    {
        hubs.Remove(hub);
        if (airports.Contains(hub)) airports.Remove(hub);
        if (railyards.Contains(hub)) railyards.Remove(hub);
        if (seaports.Contains(hub)) seaports.Remove(hub);
    }
}
