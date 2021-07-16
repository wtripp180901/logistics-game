using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorage {

    void giveItem(Item item);
    bool canAcceptItem(ITEM_TYPE item);
    Item takeItem(ITEM_TYPE item);
    ITEM_TYPE[] takeableItems { get; }
    bool storageAvailable { get; }
    bool empty { get; }
    void finishUnloadLoad();
    void startUnloadLoad();
    bool isShed { get; }
    bool canProduce { get; }
}
