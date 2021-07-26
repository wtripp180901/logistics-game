using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VehicleCreatorManager {

    private static VehicleCreator creator;
    private static bool vehicleCreationMode = false;

    public static void startCreation(VehicleCreator _creator)
    {
        creator = _creator;
        vehicleCreationMode = true;
    }
	
	// Update is called once per frame
	public static void Update () {

        if (vehicleCreationMode)
        {
            if (InputAdapter.clickInput == INPUT.UP)
            {
                creator.routeCreation();
            }
        }
    }

    public static void finish()
    {
        creator.createVehicle();
        vehicleCreationMode = false;
        creator = null;
        //ModelReciever.finishCreationAction();
    }
}
