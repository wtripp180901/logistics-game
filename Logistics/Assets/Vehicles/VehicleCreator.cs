﻿using System.Collections;
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
        VehicleManager.addVehicle(new Vehicle(new LinkedList<Journey>(finaliseRoute())));
        vehicleCreationMode = false;
    }

    private static List<Journey> finaliseRoute()
    {
        List<Journey> journies = new List<Journey>();
        TransportHubFeature firstDest = hubs.Pop();
        journies.Add(retrieveFromCache(hubs.Pop(), firstDest));
        while (hubs.Count > 0)
        {
            journies.Add(retrieveFromCache(hubs.Pop(), journies[journies.Count - 1].source));
        }
        int lastJourneyIndex = journies.Count - 1;

        journies.Reverse();
        if (journies.Count > 1 && journies[0].source == journies[lastJourneyIndex].destination)
        {
            //If the player has input a route which naturally gives a loop, return as is
            foreach(Journey j in journies)
            {
                Debug.Log(j.source.parent.position + " " + j.destination.parent.position);
            }
            return journies;
        }
        else
        {
            for(int i = 1;i < lastJourneyIndex - 1; i++)
            {
                if(journies[lastJourneyIndex].destination == journies[i].source)
                {
                    //Player's route partially forms a natural loop, return to the start point from the loop point
                    Debug.Log("Partial loop");
                    addReverseJourniesFromIndex(i - 1, journies);
                    return journies;
                }
            }
            //If the player's route is a line with no loop, add the route in reverse to the end of journies
            Debug.Log("Line");
            addReverseJourniesFromIndex(lastJourneyIndex,journies);
            return journies;
        }
    }

    //Helper for ^
    private static void addReverseJourniesFromIndex(int returnPoint,List<Journey> originalJournies)
    {
        List<Journey> newJourneys = new List<Journey>();
        for (int i = returnPoint; i >= 0; i--)
        {
            newJourneys.Add(retrieveFromCache(originalJournies[i].destination, originalJournies[i].source));
        }
        originalJournies.AddRange(newJourneys);
    }

    private static Journey retrieveFromCache(TransportHubFeature source, TransportHubFeature destination)
    {
        for(int i = 0;i < journeyCache.Count; i++)
        {
            if (journeyCache[i].source == source && journeyCache[i].destination == destination) return journeyCache[i];
        }
        throw new System.Exception("journey not cached");
    }

    //Gets hubs with paths to the current hub, caching newly computed paths along the way
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
                            List<Vector2> reversedPath = new List<Vector2>();
                            reversedPath.AddRange(path);
                            reversedPath.Reverse();
                            journeyCache.Add(new Journey(allHubs[i], currentHub,reversedPath));
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
