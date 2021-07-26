using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleCreator {

    public enum VEHICLE_TYPES { TRAIN, PLANE }

    private Map map;
    private Stack<TransportHubFeature> hubs = new Stack<TransportHubFeature>();
    private TransportHubFeature[] allHubs;
    private TransportHubFeature[] availableHubs;
    private Dictionary<sourceDest,Journey> journeyCache = new Dictionary<sourceDest, Journey>();
    private int stopCount = 1;
    protected abstract VEHICLE vehicleType { get; }

    private struct sourceDest : System.IEquatable<sourceDest>
    {
        TransportHubFeature source;
        TransportHubFeature destination;
        public sourceDest(TransportHubFeature source,TransportHubFeature destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public bool Equals(sourceDest s)
        {
            return source == s.source && destination == s.destination;
        }
    }


    public VehicleCreator(Map map)
    {
        this.map = map;
        allHubs = getAllRelevantHubs();
        availableHubs = allHubs;
        hubs.Clear();
        journeyCache.Clear();
    }

    protected abstract TransportHubFeature[] getAllRelevantHubs();

    //Pushes and pops stops on journey to hubs
    public void routeCreation()
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
                    ModelReciever.removeStopNumberUI(hub);
                    stopCount -= 1;
                }
                else if (System.Array.Exists(availableHubs, x => x == hub))
                {
                    ModelReciever.createStopNumberUI(Assets.stopNumberPrefab, hub,stopCount.ToString());
                    hubs.Push(hub);
                    stopCount += 1;
                }
                setAvailableHubs();
                ConfirmationManager.requestConfirmation(hubs.Peek().parent.position, vehicleType);
            }
        }
    }

    public void createVehicle()
    {
        VehicleManager.addVehicle(new Vehicle(new LinkedList<Journey>(finaliseRoute())));
    }

    private List<Journey> finaliseRoute()
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
    private void addReverseJourniesFromIndex(int returnPoint,List<Journey> originalJournies)
    {
        List<Journey> newJourneys = new List<Journey>();
        for (int i = returnPoint; i >= 0; i--)
        {
            Journey j = retrieveFromCache(originalJournies[i].destination, originalJournies[i].source);
            j.playerSpecified = false;
            newJourneys.Add(j);
        }
        Journey lastJourney = newJourneys[newJourneys.Count - 1];
        lastJourney.playerSpecified = true;
        newJourneys[newJourneys.Count - 1] = lastJourney;
        originalJournies.AddRange(newJourneys);
    }

    /*private static Journey retrieveFromCache(TransportHubFeature source, TransportHubFeature destination)
    {
        for(int i = 0;i < journeyCache.Count; i++)
        {
            if (journeyCache[i].source == source && journeyCache[i].destination == destination) return journeyCache[i];
        }
        throw new System.Exception("journey not cached");
    }*/
    private Journey retrieveFromCache(TransportHubFeature source, TransportHubFeature destination)
    {
        Journey j;
        if (journeyCache.TryGetValue(new sourceDest(source, destination), out j))
        {
            return j;
        }
        else throw new System.Exception("Journey not in cache");
    }

    //Gets hubs with paths to the current hub, caching newly computed paths along the way as well as the journey in reverse
    private void setAvailableHubs()
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
                    bool cached = journeyCache.ContainsKey(new sourceDest(currentHub, allHubs[i]));
                    if (cached)
                    {
                        hubsWithPath.Add(allHubs[i]);
                    }
                    else
                    {
                        List<Vector2> path = getPathOrNull(currentHub, allHubs[i]);
                        if(path != null)
                        {
                            hubsWithPath.Add(allHubs[i]);
                            journeyCache.Add(new sourceDest(currentHub,allHubs[i]),new Journey(currentHub, allHubs[i],path));
                            List<Vector2> reversedPath = new List<Vector2>();
                            reversedPath.AddRange(path);
                            reversedPath.Reverse();
                            journeyCache.Add(new sourceDest(allHubs[i],currentHub),new Journey(allHubs[i], currentHub,reversedPath));
                        }
                    }
                }
            }
            availableHubs = hubsWithPath.ToArray();
        }
    }

    protected abstract List<Vector2> getPathOrNull(TransportHubFeature source, TransportHubFeature destination);

    /*private static bool journeyCached(TransportHubFeature source,TransportHubFeature destination)
    {
        for (int i = 0; i < journeyCache.Count; i++)
        {
            if ((journeyCache[i].source == source && journeyCache[i].destination == destination) ||
                (journeyCache[i].source == destination && journeyCache[i].destination == source)) return true;
        }
        return false;
    }*/
}
