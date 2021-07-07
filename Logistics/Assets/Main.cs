using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    Map map;

    // Use this for initialization
    void Start () {
        map = new Map(new MapGenerator(2, 900, 10, 10));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            DrawingManager.startDrawing(new ConnectionDrawer(map,FEATURES.RAIL));
        }
        if (Input.GetKeyDown("e"))
        {
            DrawingManager.startDrawing(new HubDrawer(map, FEATURES.RAILYARD));
        }
        if (Input.GetKeyDown("s")) map.updateRoutes();
        DrawingManager.Update(map);
	}
}
