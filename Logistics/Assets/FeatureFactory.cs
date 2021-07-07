﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FEATURES { ROAD, RAIL, RAILYARD, NONE }

public static class FeatureFactory {

	public static Feature build(FEATURES featureType,bool temporary,Tile parent)
    {
        switch (featureType)
        {
            case FEATURES.ROAD:
            case FEATURES.RAIL:
                return new TransportFeature(featureType,temporary,parent);
            case FEATURES.RAILYARD:
                return new TransportHubFeature(featureType, temporary, parent, new FEATURES[2] { FEATURES.RAIL, FEATURES.ROAD });
            default: throw new System.Exception("trying to create feature with undefined type");
        }
    }
}
