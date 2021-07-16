using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerProducer : IStorage {

    private List<Item> products = new List<Item>();
    private List<Item> materials = new List<Item>();
    private readonly int materialLimit;
    private readonly int productLimit;
    private readonly ITEM_TYPE[] consumes;
    private readonly ITEM_TYPE produces;
    private bool unloadLoadInProgress = false;

    private ProductCreator producer;
    public bool canProduce { get { return materials.Count > 0 && products.Count < productLimit; } }

    public ConsumerProducer(ITEM_TYPE[] consumes,ITEM_TYPE produces,int materialLimit,int productLimit)
    {
        this.consumes = consumes;
        this.produces = produces;
        this.materialLimit = materialLimit;
        this.productLimit = productLimit;
        producer = new ConsumerProducerProductCreator(this,materials,products,produces);
    }

    public Item takeItem(ITEM_TYPE item)
    {
        for (int i = products.Count - 1; i >= 0; i--)
        {
            if (products[i].type == item)
            {
                Item toReturn = products[i];
                products.RemoveAt(i);
                return toReturn;
            }
        }
        throw new System.Exception("Trying to take non existant item");
    }

    public void giveItem(Item item)
    {
        materials.Add(item);
    }

    public bool canAcceptItem(ITEM_TYPE type)
    {
        return materials.Count < materialLimit && System.Array.Exists(consumes,x => x == type);
    }

    public ITEM_TYPE[] takeableItems
    {
        get {
            if (products.Count == 0) return new ITEM_TYPE[0];
            else return new ITEM_TYPE[1] { produces };
        }
    }
    public bool empty { get { return products.Count == 0; } }
    public bool storageAvailable { get { return materials.Count < materialLimit; } }

    public void startUnloadLoad()
    {
        Debug.Log("starting delivery");
        unloadLoadInProgress = true;
    }
    public void finishUnloadLoad()
    {
        Debug.Log("finishing delivery");
        unloadLoadInProgress = false;
    }
    public bool isShed { get { return false; } }
}
