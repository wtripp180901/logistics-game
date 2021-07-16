using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VehicleManager {

    private static List<Vehicle> vehicles = new List<Vehicle>();

	public static void Update () {
		for(int i = 0;i < vehicles.Count; i++)
        {
            vehicles[i].Update();
        }
	}

    public static void addVehicle(Vehicle v) { vehicles.Add(v); }
}
