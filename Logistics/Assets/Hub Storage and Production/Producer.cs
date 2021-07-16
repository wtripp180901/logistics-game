using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : IStorage {

    private List<Item> items = new List<Item>();
    private ITEM_TYPE produces;
    private int productionLimit;
    private ProducerProductCreator productCreator;

    public Producer(ITEM_TYPE produces,int productionLimit)
    {
        this.produces = produces;
        this.productionLimit = productionLimit;
        productCreator = new ProducerProductCreator(this, items, produces);
    }

    public Item takeItem(ITEM_TYPE item)
    {
        Item toReturn = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return toReturn;
    }
    public ITEM_TYPE[] takeableItems { get {
            if (items.Count > 0)
                return new ITEM_TYPE[1] { produces };
            else return new ITEM_TYPE[0];
        }
    }

    public void startUnloadLoad() { Debug.Log("starting delivery"); }
    public void finishUnloadLoad() { Debug.Log("finishing delivery"); }

    public bool canProduce { get { return items.Count < productionLimit; } }
    public bool isShed { get { return false; } }
    public bool empty { get { return items.Count == 0; } }
    public bool storageAvailable { get { return false; } }
	public void giveItem(Item item) { throw new System.Exception("Trying to give item to producer"); }
    public bool canAcceptItem(ITEM_TYPE item) { return false; }
}
