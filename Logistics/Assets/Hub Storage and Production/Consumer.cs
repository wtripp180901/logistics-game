using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : IStorage {

    private bool unloadInProgress = false;
    private readonly int itemValue;
    private ITEM_TYPE consumes;

    public Consumer(ITEM_TYPE consumes,int itemValue)
    {
        this.itemValue = itemValue;
        this.consumes = consumes;
    }

    public Item takeItem(ITEM_TYPE item) { throw new System.Exception("Shouldn't be taking items from consumer"); }
    public ITEM_TYPE[] takeableItems { get { return new ITEM_TYPE[0]; } }
    public bool storageAvailable { get { return true; } }
    public bool isShed { get { return false; } }
    public bool empty { get { return true; } }
    public bool canProduce { get { return true; } }
    public bool canAcceptItem(ITEM_TYPE item) { return item == consumes; }
    public void giveItem(Item item) { MoneyManager.addMoney(itemValue); }

    public void startUnloadLoad()
    {
        unloadInProgress = true;
    }
    public void finishUnloadLoad()
    {
        unloadInProgress = false;
    }
}
