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
        storage = makeStorage(featureType);
    }

    public override bool canLinkWith(FEATURES feat)
    {
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

    private static IStorage makeStorage(FEATURES featureType)
    {
        switch (featureType)
        {
            case FEATURES.RAILYARD:
                return new Shed(8);
            case FEATURES.FOOD_FACTORY:
                return new ConsumerProducer(new ITEM_TYPE[1] { ITEM_TYPE.MEAT }, ITEM_TYPE.FOOD, 4, 4);
            default: throw new System.Exception("not a hub feature");
        }
    }
}
