using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerProductCreator : ProductCreator {

    private HubUIWrapper UI;

    public ProducerProductCreator(IStorage parent, List<Item> products, ITEM_TYPE produces,HubUIWrapper UI) : base(parent, products, produces) { this.UI = UI; }

    protected override void produce()
    {
        products.Add(new Item(produces));
        UI.addItem(produces, 1);
    }
}
