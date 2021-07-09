using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportHubFeature : TransportFeature {

    public override bool isHub{ get {return true;} }
    public TransportHubFeature(FEATURES featureType, bool temporary, Tile parent): base(featureType, temporary, parent){ linkCapacity = 4; }
    private Dictionary<TransportHubFeature, List<Vector2>> routes = new Dictionary<TransportHubFeature, List<Vector2>>();
    private FEATURES[] allowedLinks;

    public TransportHubFeature(FEATURES featureType, bool temporary, Tile parent,FEATURES[] allowedLinks) : base(featureType, temporary, parent)
    {
        this.allowedLinks = allowedLinks;
    }

    public override bool canLinkWith(FEATURES feat)
    {
        Debug.Log("based polymorphism");
        for(int i = 0;i < allowedLinks.Length; i++)
        {
            if (allowedLinks[i] == feat) return true;
        }
        return false;
    }

    protected override GameObject getSprite()
    {
        return Assets.railyardPrefab;
    }
}
