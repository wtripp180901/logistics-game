using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HubObserver {

    private static List<TransportHubFeature> hubs = new List<TransportHubFeature>(); public static TransportHubFeature[] getHubs { get { return hubs.ToArray(); } }
    private static List<TransportHubFeature> airports = new List<TransportHubFeature>(); public static TransportHubFeature[] getAirports { get { return airports.ToArray(); } }

    public static void subscribe(TransportHubFeature hub)
    {
        hubs.Add(hub);
        if (hub.featureType == FEATURES.AIRPORT) airports.Add(hub);
    }

    public static void unsubscribe(TransportHubFeature hub)
    {
        hubs.Remove(hub);
        if (airports.Contains(hub)) airports.Add(hub);
    }
}
