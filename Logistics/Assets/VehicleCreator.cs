using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VehicleCreator {

    public enum VEHICLE_TYPES { TRAIN, PLANE }

    private static bool vehicleCreationMode = false;
    private static Map map;
    private static Stack<TransportHubFeature> hubs = new Stack<TransportHubFeature>();
    private static TransportHubFeature[] allHubs;
    private static TransportHubFeature[] availableHubs;
    private static List<Journey> journeyCache = new List<Journey>();


    public static void startVehicleCreation(Map _map)
    {
        vehicleCreationMode = true;
        map = _map;
        allHubs = map.getHubs();
        availableHubs = allHubs;
        hubs.Clear();
        journeyCache.Clear();
    }

    public static void Update()
    {
        if (vehicleCreationMode)
        {
            if(InputAdapter.clickInput == INPUT.UP)
            {
                routeCreation();
            }
            if (Input.GetKeyDown("z"))
            {
                createVehicle();
            }
        }
    }

    //Helper
    private static void routeCreation()
    {
        Tile currentTile = map.tileWithMouseInside();
        if (currentTile != null)
        {
            Feature currentFeature = currentTile.getFeature(false);
            if (currentFeature != null && currentFeature.isHub)
            {
                TransportHubFeature hub = (TransportHubFeature)currentFeature;
                if (hubs.Count > 0 && hubs.Peek() == hub)
                {
                    //Double clicking current hub removes it
                    hubs.Pop();
                    Debug.Log("pop");
                    setAvailableHubs();
                }
                else if (System.Array.Exists(availableHubs, x => x == hub))
                {
                    hubs.Push(hub);
                    setAvailableHubs();
                    Debug.Log("push");
                }
            }
        }
    }

    private static void createVehicle()
    {
        List<Journey> journies = new List<Journey>();
        TransportHubFeature firstDest = hubs.Pop();
        journies.Add(retrieveFromCache(hubs.Pop(), firstDest));
        while(hubs.Count > 0)
        {
            journies.Add(retrieveFromCache(hubs.Pop(), journies[journies.Count - 1].source));
        }
        journies.Reverse();
        VehicleManager.addVehicle(new Vehicle(journies));
        vehicleCreationMode = false;
    }

    private static Journey retrieveFromCache(TransportHubFeature source, TransportHubFeature destination)
    {
        for(int i = 0;i < journeyCache.Count; i++)
        {
            if (journeyCache[i].source == source && journeyCache[i].destination == destination) return journeyCache[i];
        }
        throw new System.Exception("journey not cached");
    }

    private static void setAvailableHubs()
    {
        if (hubs.Count == 0)
        {
            availableHubs = allHubs;
        }
        else
        {
            TransportHubFeature currentHub = hubs.Peek();
            List<TransportHubFeature> hubsWithPath = new List<TransportHubFeature>();
            for (int i = 0; i < allHubs.Length; i++)
            {
                if(allHubs[i] != currentHub)
                {
                    bool cached = journeyCached(currentHub, allHubs[i]);
                    if (cached)
                    {
                        hubsWithPath.Add(allHubs[i]);
                    }
                    else
                    {
                        List<Vector2> path = RouteFinder.findPath(currentHub, allHubs[i]);
                        if(path != null)
                        {
                            hubsWithPath.Add(allHubs[i]);
                            journeyCache.Add(new Journey(currentHub, allHubs[i],path));
                            journeyCache.Add(new Journey(allHubs[i], currentHub,path));
                        }
                    }
                }
            }
            availableHubs = hubsWithPath.ToArray();
        }
    }

    private static bool journeyCached(TransportHubFeature source,TransportHubFeature destination)
    {
        for (int i = 0; i < journeyCache.Count; i++)
        {
            if ((journeyCache[i].source == source && journeyCache[i].destination == destination) ||
                (journeyCache[i].source == destination && journeyCache[i].destination == source)) return true;
        }
        return false;
    }
}
