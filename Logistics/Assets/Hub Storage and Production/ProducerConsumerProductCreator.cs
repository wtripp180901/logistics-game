using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerProducerProductCreator : ProductCreator {

    private List<Item> materials;
    private HubUIWrapper UI;

    public ConsumerProducerProductCreator(ConsumerProducer parent,List<Item> materials, List<Item> products, ITEM_TYPE produces,HubUIWrapper UI) : base(parent, products, produces)
    {
        this.materials = materials;
        this.UI = UI;
    }

    protected override void produce()
    {
        UI.addItem(materials[materials.Count - 1].type, -1);
        materials.RemoveAt(materials.Count - 1);
        products.Add(new Item(produces));
        UI.addItem(produces, 1);
    }
}
