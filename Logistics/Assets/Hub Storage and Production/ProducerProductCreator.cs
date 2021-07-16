using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerProductCreator : ProductCreator {

    public ProducerProductCreator(IStorage parent, List<Item> products, ITEM_TYPE produces) : base(parent, products, produces) { }

    protected override void produce()
    {
        products.Add(new Item(produces));
    }
}
