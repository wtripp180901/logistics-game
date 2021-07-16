using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductCreator {

    private IStorage parent;
    private readonly float timerInterval = 3f;
    private float timer = 0f;
    protected ITEM_TYPE produces;
    protected List<Item> products;

    public ProductCreator(IStorage parent,List<Item> products, ITEM_TYPE produces)
    {
        ProductCreatorObserver.subscribe(this);
        this.parent = parent;
        this.produces = produces;
        this.products = products;
    }

    public void Update()
    {
        if(parent.canProduce) timer += Time.deltaTime;
        if (timer >= timerInterval)
        {
            timer -= timerInterval;
            produce();
        }
    }

    protected abstract void produce();
}
