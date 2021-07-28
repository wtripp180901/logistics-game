﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : IStorage {

    private List<Item> items = new List<Item>();
    private ITEM_TYPE produces;
    private int productionLimit;
    private ProducerProductCreator productCreator;
    private HubUIWrapper UI;

    public Producer(ITEM_TYPE produces,int productionLimit,Vector2 UIPosition)
    {
        this.produces = produces;
        this.productionLimit = productionLimit;
        UI = new HubUIWrapper(produces, UIPosition);
        UI.setValue(produces, items.Count);
        productCreator = new ProducerProductCreator(this, items, produces, UI);
    }

    public Item takeItem(ITEM_TYPE item)
    {
        Item toReturn = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        UI.setValue(item, items.Count);
        return toReturn;
    }
    public ITEM_TYPE[] takeableItems { get {
            if (items.Count > 0)
                return new ITEM_TYPE[1] { produces };
            else return new ITEM_TYPE[0];
        }
    }

    public void startUnloadLoad() {  }
    public void finishUnloadLoad() {  }

    public bool canProduce { get { return items.Count < productionLimit; } }
    public bool isShed { get { return false; } }
    public bool empty { get { return items.Count == 0; } }
    public bool storageAvailable { get { return false; } }
	public void giveItem(Item item) { throw new System.Exception("Trying to give item to producer"); }
    public bool canAcceptItem(ITEM_TYPE item) { return false; }
}
