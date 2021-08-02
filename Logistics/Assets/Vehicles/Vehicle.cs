using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VEHICLE { VAN, TRAIN, PLANE}

public class Vehicle {
    private LinkedList<Journey> routes;
    public GameObject gameObject;
    public Transform transform;
    private float speed = 1f;

    private LinkedListNode<Journey> currentJourneyNode;
    private List<Vector2> currentPath { get { return currentJourneyNode.Value.path; } }
    private TransportHubFeature currentDestination { get { return currentJourneyNode.Value.destination; } }
    private int currentPathIndex = 0;
    private Vector2 currentVectorTarget;
    private Vector2 travelVector = new Vector2(0, 0);

    private List<Item> items = new List<Item>();
    private int itemCapacity = 2;
    private const float loadInterval = 0.5f;
    private float loadTimer = 0f;

    public Vehicle(LinkedList<Journey> routes)
    {
        this.routes = routes;
        const float vehicleLayer = -0.15f;
        gameObject = Object.Instantiate(Assets.assets.trainPrefab, (Vector3)routes.First.Value.source.parent.position + new Vector3(0,0,vehicleLayer), Quaternion.identity);
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
        if (currentJourneyNode.Value.playerSpecified && hubCanAcceptAtLeastOneItem(currentDestination))
        {
            loadTimer += Time.deltaTime;
            if (loadTimer >= loadInterval)
            {
                loadTimer -= loadInterval;
                for (int i = items.Count - 1; i >= 0; i--)
                {
                    if (currentDestination.storage.canAcceptItem(items[i].type))
                    {
                        currentDestination.storage.giveItem(items[i]);
                        Debug.Log("unloaded " + items[i].type);
                        items.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        else
        {
            loadTimer = 0;
            stage = STAGE.LOADING;
        }
    }

    private void load()
    {
        if (currentJourneyNode.Value.playerSpecified && canTakeAtLeastOneItemFromHub(currentDestination))
        {
            loadTimer += Time.deltaTime;
            if(loadTimer >= loadInterval)
            {
                loadTimer -= loadInterval;
                ITEM_TYPE[] hubItems = currentDestination.storage.takeableItems;
                for(int i = hubItems.Length - 1;i >= 0; i--)
                {
                    if(consumerOnRoute(hubItems[i]))
                    {
                        items.Add(currentDestination.storage.takeItem(hubItems[i]));
                        Debug.Log("loaded " + hubItems[i]);
                        break;
                    }
                }
            }
        }
        else
        {
            currentDestination.storage.finishUnloadLoad();
            if (currentJourneyNode == routes.Last) currentJourneyNode = routes.First;
            else currentJourneyNode = currentJourneyNode.Next;
            stage = STAGE.TRAVEL;
        }
    }

    private bool hubCanAcceptAtLeastOneItem(TransportHubFeature hub)
    {
        if (!hub.storage.storageAvailable)
        {
            return false;
        }
        for (int i = 0;i < items.Count; i++)
        {
            if (hub.storage.canAcceptItem(items[i].type)) return true;
        }
        return false;
    }

    private bool canTakeAtLeastOneItemFromHub(TransportHubFeature hub)
    {
        bool itemFromHubHasConsumer = false;
        ITEM_TYPE[] hubItems = hub.storage.takeableItems;
        for (int i = 0;i < hubItems.Length; i++)
        {
            if (consumerOnRoute(hubItems[i]))
            {
                itemFromHubHasConsumer = true;
                break;
            }
        }
        return items.Count < itemCapacity && !currentDestination.storage.empty && itemFromHubHasConsumer;
    }

    private void travel()
    {
        if ((currentVectorTarget - (Vector2)transform.position).magnitude > 0.05f)
        {
            transform.position += (Vector3)travelVector * Time.deltaTime;
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
                currentDestination.storage.startUnloadLoad();
                currentPathIndex = 0;
            }
        }
    }

    private bool consumerOnRoute(ITEM_TYPE type)
    {
        /*LinkedListNode<Journey> node = currentJourneyNode.Next;
        bool firstNodeIsShed = currentJourneyNode.Value.destination.storage.isShed;
        while (node != null)
        {
            IStorage currentStorage = node.Value.destination.storage;
            if (currentStorage.canAcceptItem(type) && !(firstNodeIsShed && currentStorage.isShed)) return true;
            node = node.Next;
        }
        return false;*/
        //Rewrite ^ to go through entire journey if destination is first node
        LinkedListNode<Journey> node = routes.First;
        while(node != null)
        {
            if(node != currentJourneyNode)
            {
                IStorage nodeStorage = node.Value.destination.storage;
                if (nodeStorage.canAcceptItem(type) && (!(currentDestination.storage.isShed && nodeStorage.isShed) || (currentDestination.isPort && node.Value.destination.isPort))) return true;
            }
            node = node.Next;
        }
        return false;
    }
}
