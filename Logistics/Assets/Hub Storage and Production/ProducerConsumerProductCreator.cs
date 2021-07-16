using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerProducerProductCreator : ProductCreator {

    private List<Item> materials;

    public ConsumerProducerProductCreator(ConsumerProducer parent,List<Item> materials, List<Item> products, ITEM_TYPE produces) : base(parent, products, produces)
    {
        this.materials = materials;
    }

    protected override void produce()
    {
        materials.RemoveAt(materials.Count - 1);
        products.Add(new Item(produces));
    }
}
