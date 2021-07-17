using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StorageFactory {

	public static IStorage build(FEATURES featureType)
    {
        switch (featureType)
        {
            case FEATURES.RAILYARD:
                return new Shed(8);
            case FEATURES.FOOD_FACTORY:
                return new ConsumerProducer(new ITEM_TYPE[1] { ITEM_TYPE.MEAT }, ITEM_TYPE.FOOD, 4, 4);
            case FEATURES.FARM:
                return new Producer(ITEM_TYPE.MEAT, 4);
            case FEATURES.MARKET:
                return new Consumer(ITEM_TYPE.FOOD, 20);
            default: throw new System.Exception("not a hub feature");
        }
    }
}
