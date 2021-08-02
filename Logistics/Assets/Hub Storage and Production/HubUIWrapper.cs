using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubUIWrapper {

    private Dictionary<ITEM_TYPE, Text> itemUI = new Dictionary<ITEM_TYPE, Text>();
    private List<GameObject> allUI = new List<GameObject>();

    private static readonly Dictionary<ITEM_TYPE, Sprite> iconsForItemType = new Dictionary<ITEM_TYPE, Sprite>()
    {

    };

    public HubUIWrapper(ITEM_TYPE[] materials,ITEM_TYPE[] products,Vector2 position)
    {
        const float xOffsetInterval = 0.5f;
        Vector2 offsetVector = new Vector2(xOffsetInterval, 0);
        for(int i = 0;i < materials.Length; i++)
        {
            createUIElement(materials[i], position + (offsetVector * (i + 1)));
        }
        for (int i = 0; i < products.Length; i++)
        {
            createUIElement(products[i], position - (offsetVector * (i + 1)));
        }
    }

    public HubUIWrapper(ITEM_TYPE item,Vector2 position)
    {
        createUIElement(item, position);
    }

    private void createUIElement(ITEM_TYPE itemType,Vector2 position)
    {
        GameObject go = Object.Instantiate(Assets.assets.factoryUIPrefab, Assets.assets.worldCanvas);
        go.transform.position = position;
        /*Sprite sprite;
        iconsForItemType.TryGetValue(itemType, out sprite);
        go.GetComponent<SpriteRenderer>().sprite = sprite;*/

        allUI.Add(go);
        Text text = go.transform.GetChild(0).GetComponent<Text>();
        itemUI.Add(itemType, text);
    }

    public void addItem(ITEM_TYPE item,int x)
    {
        Text text;
        itemUI.TryGetValue(item, out text);
        int newValue = int.Parse(text.text) + x;
        text.text = newValue.ToString();
    }

    public void setValue(ITEM_TYPE item,int x)
    {
        Text text;
        itemUI.TryGetValue(item, out text);
        text.text = x.ToString();
    }
}
