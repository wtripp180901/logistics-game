using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle {
    private LinkedList<Journey> routes;
    public GameObject gameObject;
    public Transform transform;
    private float speed = 0.1f;

    private LinkedListNode<Journey> currentJourneyNode;
    private List<Vector2> currentPath {  get { return currentJourneyNode.Value.path; } }
    private int currentPathIndex = 0;
    private Vector2 currentVectorTarget;
    private Vector2 travelVector = new Vector2(0,0);

    public Vehicle(LinkedList<Journey> routes)
    {
        this.routes = routes;
        const float vehicleLayer = -0.15f;
        gameObject = Object.Instantiate(Assets.trainPrefab, (Vector3)routes.First.Value.source.parent.position + new Vector3(0,0,vehicleLayer), Quaternion.identity);
        transform = gameObject.transform;
        currentJourneyNode = this.routes.First;
        currentVectorTarget = currentJourneyNode.Value.path[0];
    }

    private enum STAGE { LOADING, UNLOADING, TRAVEL }
    private STAGE stage = STAGE.TRAVEL;

    public void Update()
    {
        switch (stage)
        {
            case STAGE.LOADING:
                load();
                break;
            case STAGE.UNLOADING:
                unload();
                break;
            case STAGE.TRAVEL:
                travel();
                break;
        }
    }

    private void unload()
    {
        load();
    }

    private void load()
    {
        if (currentJourneyNode == routes.Last) currentJourneyNode = routes.First;
        else currentJourneyNode = currentJourneyNode.Next;
        stage = STAGE.TRAVEL;
    }

    private void travel()
    {
        if ((currentVectorTarget - (Vector2)transform.position).magnitude > 0.05f)
        {
            transform.position += (Vector3)travelVector * speed;
        }
        else
        {
            currentPathIndex += 1;
            if (currentPathIndex < currentPath.Count)
            {
                travelVector = (currentPath[currentPathIndex] - currentVectorTarget).normalized * speed;
                currentVectorTarget = currentPath[currentPathIndex];
            }
            else
            {
                stage = STAGE.UNLOADING;
                currentPathIndex = 0;
            }
        }
    }
}
