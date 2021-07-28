using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FEATURES { ROAD, RAIL, RAILYARD, FOOD_FACTORY,FARM,MARKET,AIRPORT,SEAPORT,NONE }

public static class FeatureFactory {

	public static Feature build(FEATURES featureType,bool temporary,Tile parent)
    {
        switch (featureType)
        {
            case FEATURES.ROAD:
            case FEATURES.RAIL:
                return new TransportFeature(featureType,temporary,parent);
            case FEATURES.RAILYARD:
            case FEATURES.AIRPORT:
                return new TransportHubFeature(featureType, temporary, parent, new FEATURES[2] { FEATURES.RAIL, FEATURES.ROAD },makeStorage(featureType,parent));
            case FEATURES.FOOD_FACTORY:
            case FEATURES.FARM:
            case FEATURES.MARKET:
                return new TransportHubFeature(featureType, temporary, parent, new FEATURES[1] { FEATURES.ROAD }, makeStorage(featureType,parent));
            default: throw new System.Exception("trying to create feature with undefined type");
        }
    }

    private static IStorage makeStorage(FEATURES featureType,Tile parent)
    {
        Vector2 UIPosition = new Vector2(parent.position.x, parent.position.y + Assets.tilePrefabWidth);
        switch (featureType)
        {
            case FEATURES.RAILYARD:
            case FEATURES.AIRPORT:
                return new Shed(8);
            case FEATURES.FOOD_FACTORY:
                return new ConsumerProducer(new ITEM_TYPE[1] { ITEM_TYPE.MEAT }, ITEM_TYPE.FOOD, 4, 4,UIPosition);
            case FEATURES.FARM:
                return new Producer(ITEM_TYPE.MEAT, 4,UIPosition);
            case FEATURES.MARKET:
                return new Consumer(ITEM_TYPE.FOOD, 20);
            default: throw new System.Exception("not a hub feature");
        }
    }
}
