using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawerFactory {

    private static Map map;

    public static void initialise(Map _map)
    {
        map = _map;
    }

	public static Drawer build(FEATURES feature)
    {
        switch (feature)
        {
            case FEATURES.RAILYARD:
            case FEATURES.AIRPORT:
                return new HubDrawer(map, feature);
            case FEATURES.RAIL:
            case FEATURES.ROAD:
                return new ConnectionDrawer(map, feature);
            default: throw new System.Exception("Not implemented: " + feature);
        }
    }
}
