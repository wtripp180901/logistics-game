using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VehicleCreatorFactory {

    public static Map map = null;

    public static void initialise(Map _map)
    {
        map = _map;
    }

	public static VehicleCreator build(VEHICLE vehicle)
    {
        if (map == null) throw new System.Exception("Factory not initialised");
        switch (vehicle)
        {
            case VEHICLE.VAN:
                return new GroundVehicleCreator(map);
            case VEHICLE.PLANE:
                return new PlaneVehicleCreator(map);
            case VEHICLE.TRAIN:
                return new TrainVehicleCreator(map);
            default: throw new System.Exception("Vehicle not implemented");
        }
    }
}
