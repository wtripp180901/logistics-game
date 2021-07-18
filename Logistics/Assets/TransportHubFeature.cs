using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportHubFeature : TransportFeature {

    public override bool isHub{ get {return true;} }
    public TransportHubFeature(FEATURES featureType, bool temporary, Tile parent): base(featureType, temporary, parent){ linkCapacity = 4; }
    private FEATURES[] allowedLinks;
    public readonly IStorage storage;

    public TransportHubFeature(FEATURES featureType, bool temporary, Tile parent,FEATURES[] allowedLinks) : base(featureType, temporary, parent)
    {
        this.allowedLinks = allowedLinks;
        storage = StorageFactory.build(featureType);
    }

    public override bool canLinkWith(TransportFeature feat)
    {
        Debug.Log("poly");
        for (int i = 0;i < allowedLinks.Length; i++)
        {
            if (allowedLinks[i] == feat.featureType) return true;
        }
        return false;
    }

    protected override GameObject getSprite()
    {
        return Assets.railyardPrefab;
    }
}
