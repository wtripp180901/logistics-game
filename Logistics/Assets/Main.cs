using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    Map map;

    // Use this for initialization
    void Start () {
        map = new Map(new MapGenerator(2, 900, 10, 10));
    }

    TransportHubFeature source;
    TransportHubFeature dest;

	// Update is called once per frame
	void Update () {
        //Debug shit**********************************
        if (Input.GetKeyDown("q"))
        {
            DrawingManager.startDrawing(new ConnectionDrawer(map,FEATURES.RAIL));
        }
        if (Input.GetKeyDown("e"))
        {
            DrawingManager.startDrawing(new HubDrawer(map, FEATURES.RAILYARD));
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
        if (Input.GetKeyDown("f")) VehicleCreator.startVehicleCreation(map);
        if (Input.GetMouseButtonDown(1))
        {
            TransportFeature feat = (TransportFeature)map.tileWithMouseInside().getFeature(false);
            foreach(Feature f in feat.links)
            {
                Debug.Log(f.parent.position);
            }
        }
        //****************************************************
        DrawingManager.Update(map);
        VehicleCreator.Update();
        VehicleManager.Update();
	}
}
