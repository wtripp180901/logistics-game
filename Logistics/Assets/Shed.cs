using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shed : IStorage {

    private List<Item> items = new List<Item>();
    private List<Item> temporaryStorage = new List<Item>();
    private int itemLimit;
    private bool unloadLoadInProgress = false;

    public Shed(int itemLimit)
    {
        this.itemLimit = itemLimit;
    }

    public void giveItem(Item item)
    {
        temporaryStorage.Add(item);
    }

    public void startUnloadLoad()
    {
        if (!unloadLoadInProgress) Debug.Log("starting delivery");
        unloadLoadInProgress = true;
    }

    public void finishUnloadLoad()
    {
        if (unloadLoadInProgress)
        {
            Debug.Log("finishing delivery");
            items.AddRange(temporaryStorage);
        }
        unloadLoadInProgress = false;
    }

    public ITEM_TYPE[] takeableItems
    {
        get {
            List<ITEM_TYPE> takeables = new List<ITEM_TYPE>();
            for (int i = 0; i < items.Count; i++)
            {
                if (!takeables.Contains(items[i].type)) takeables.Add(items[i].type);
            }
            return takeables.ToArray();
        }
    }

    public Item takeItem(ITEM_TYPE item)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].type == item)
            {
                Item toReturn = items[i];
                items.RemoveAt(i);
                return toReturn;
            }
        }
        throw new System.Exception("Trying to take unavailable item");
    }

    public bool storageAvailable { get { return items.Count + temporaryStorage.Count < itemLimit; } }
    public bool empty { get { return items.Count == 0; } }
    public bool canAcceptItem(ITEM_TYPE type){ return storageAvailable; }
    public bool isShed { get { return true; } }
}
