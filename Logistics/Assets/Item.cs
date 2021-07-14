using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE { MEAT, FOOD }

public class Item {

    public readonly ITEM_TYPE type;

    public Item(ITEM_TYPE type)
    {
        this.type = type;
    }
}
