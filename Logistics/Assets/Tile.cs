using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    public GameObject gameObject;
    private Feature _feature = null;
    public Feature getFeature(bool requesterIsTemporary)
    {
        if (_feature != null && (!_feature.temporary || (_feature.temporary && requesterIsTemporary))) return _feature;
        else return null;
    }
    public readonly Vector2 position;

    public Tile(float x,float y)
    {
        position = new Vector2(x, y);
    }

    public Tile[] adjacentTiles = new Tile[4] { null, null, null, null };
    public Tile left { get { return adjacentTiles[0]; } set { adjacentTiles[0] = value;} }
    public Tile right { get { return adjacentTiles[1]; } set { adjacentTiles[1] = value; } }
    public Tile up { get { return adjacentTiles[2]; } set { adjacentTiles[2] = value; } }
    public Tile down { get { return adjacentTiles[3]; } set { adjacentTiles[3] = value; } }

    public bool adjacentTo(Tile tile)
    {
        for(int i = 0;i < adjacentTiles.Length; i++)
        {
            if (adjacentTiles[i] == tile) return true;
        }
        return false;
    }

    public void addFeature(FEATURES toAdd,bool temporary)
    {
        if (_feature == null)
        {
            _feature = FeatureFactory.build(toAdd, temporary, this);
            if (_feature.isTransportFeature)
            {
                TransportFeature t = (TransportFeature)_feature;
                t.link();
            }
            renderFeature();
        }
    }

    public void removeFeature()
    {
        if(_feature != null && _feature.isHub) HubObserver.unsubscribe((TransportHubFeature)_feature);
    }

    public void confirmFeature()
    {
        _feature.temporary = false;
        renderFeature();
    }

    //Instantiates the GameObject representing this tile
    public void render()
    {
        gameObject = Object.Instantiate(Assets.allsidesPrefab, position, Quaternion.identity);
    }

    public void renderFeature()
    {
        if(_feature != null)
        {
            _feature.render();
        }
    }

    private bool mouseOverMode = false;
    public void setMouseOverBehaviour(bool mouseOverMode)
    {
        if(this.mouseOverMode != mouseOverMode)
        {
            if (mouseOverMode)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            }
            this.mouseOverMode = mouseOverMode;
        }
    }

    //Returns true if point is inside tile's sprite
    private static float bounds = Assets.tilePrefabWidth / 2;
    public bool pointInside(Vector2 pos)
    {
        return
            pos.x <= position.x + bounds &&
            pos.x >= position.x - bounds &&
            pos.y <= position.y + bounds &&
            pos.y >= position.y - bounds;
    }
}
