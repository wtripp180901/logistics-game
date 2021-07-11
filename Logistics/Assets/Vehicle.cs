using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle {
    private List<Journey> routes;
    private int currentRouteIndex = 0;
    private int currentDestinationIndex = 0;
    public GameObject gameObject;
    public Vehicle(List<Journey> routes)
    {
        this.routes = routes;
        const float vehicleLayer = -0.15f;
        gameObject = Object.Instantiate(Assets.trainPrefab, (Vector3)routes[0].source.parent.position + new Vector3(0,0,vehicleLayer), Quaternion.identity);
        foreach(Journey j in routes)
        {
            foreach(Vector2 v in j.path)
            {
                Debug.Log(v);
            }
        }
    }

    public void Update()
    {

    }
}
