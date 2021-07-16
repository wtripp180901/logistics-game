using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProductCreatorObserver {

    private static List<ProductCreator> productCreators = new List<ProductCreator>();

    public static void Update()
    {
        for(int i = 0;i < productCreators.Count; i++)
        {
            productCreators[i].Update();
        }
    }

    public static void subscribe(ProductCreator producer) { productCreators.Add(producer); }
}
