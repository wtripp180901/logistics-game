using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feature {

    public GameObject gameObject;
    public bool temporary;
    public readonly Tile parent;
    private FEATURES _featureType; public FEATURES featureType { get { return _featureType; } }
    public virtual bool isTransportFeature { get { return false; } }
    public virtual bool isHub { get { return false; } }

    public Feature(FEATURES featureType, bool temporary, Tile parent)
    {
        _featureType = featureType;
        this.temporary = temporary;
        this.parent = parent;
    }

    public void render()
    {
        const float featureLayer = -0.1f;
        if (gameObject != null) Object.Destroy(gameObject);
        gameObject = Object.Instantiate(getSprite(), new Vector3(parent.position.x,parent.position.y,featureLayer), Quaternion.identity);
        if(temporary) gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    protected virtual GameObject getSprite()
    {
        return Assets.assets.railOnesidePrefab;
    }

}
