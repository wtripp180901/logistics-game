using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    Map map;

    // Use this for initialization
    void Start () {
        UIStateManager.changeState(UI_STATE.NEUTRAL);
        map = new Map(new MapGenerator(2, 900, 10, 10));
        VehicleCreatorFactory.initialise(map);
        DrawerFactory.initialise(map);
    }

    TransportHubFeature source;
    TransportHubFeature dest;

	// Update is called once per frame
	void Update () {
        //Debug shit**********************************
        if (Input.GetKeyDown("q"))
        {
            DrawingManager.startDrawing(new ConnectionDrawer(map,FEATURES.ROAD));
        }
        if (Input.GetKeyDown("p"))
        {
            DrawingManager.startDrawing(new ConnectionDrawer(map, FEATURES.RAIL));
        }
        if (Input.GetKeyDown("e"))
        {
            DrawingManager.startDrawing(new HubDrawer(map, FEATURES.FARM));
        }
        if (Input.GetKeyDown("t"))
        {
            DrawingManager.startDrawing(new HubDrawer(map, FEATURES.FOOD_FACTORY));
        }
        if (Input.GetKeyDown("y"))
        {
            DrawingManager.startDrawing(new HubDrawer(map, FEATURES.RAILYARD));
        }
        if (Input.GetKeyDown("u"))
        {
            DrawingManager.startDrawing(new HubDrawer(map, FEATURES.MARKET));
        }
        if (Input.GetKeyDown("i"))
        {
            DrawingManager.startDrawing(new HubDrawer(map, FEATURES.AIRPORT));
        }
        if (Input.GetKeyDown("s")) source = (TransportHubFeature)map.tileWithMouseInside().getFeature(false);
        if (Input.GetKeyDown("w")) dest = (TransportHubFeature)map.tileWithMouseInside().getFeature(false);
        if (Input.GetKeyDown("r"))
        {
            foreach(Vector2 v in RouteFinder.findPath(source, dest))
            {
                Debug.Log(v);
            }
        }
        if (Input.GetKeyDown("f")) VehicleCreatorManager.startCreation(new GroundVehicleCreator(map));
        if (Input.GetKeyDown("g")) VehicleCreatorManager.startCreation(new PlaneVehicleCreator(map));
        //****************************************************
        DrawingManager.Update(map);
        VehicleCreatorManager.Update();
        VehicleManager.Update();
        ProductCreatorObserver.Update();
	}
}
