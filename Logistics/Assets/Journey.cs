using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Journey {

    public TransportHubFeature source;
    public TransportHubFeature destination;
    public readonly List<Vector2> path;
    public readonly bool valid;
    public bool playerSpecified;

    public Journey(TransportHubFeature source,TransportHubFeature destination)
    {
        this.source = source;
        this.destination = destination;
        path = new GroundRouteFinder().findPath(source,destination);
        valid = path != null;
        playerSpecified = true;
    }
    public Journey(TransportHubFeature source, TransportHubFeature destination,List<Vector2> path)
    {
        this.source = source;
        this.destination = destination;
        this.path = path;
        valid = path != null;
        playerSpecified = true;
    }
}
