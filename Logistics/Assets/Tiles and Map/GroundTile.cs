using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : Tile {

    public GameObject gameObject;
    private Feature _feature = null;
    public Feature getFeature(bool requesterIsTemporary)
    {
        if (_feature != null && (!_feature.temporary || (_feature.temporary && requesterIsTemporary))) return _feature;
        else return null;
    }

    public GroundTile(float x, float y) : base(x, y) { }

    public void addFeature(FEATURES toAdd, bool temporary)
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
        if (_feature != null)
        {
            if (_feature.isHub) HubObserver.unsubscribe((TransportHubFeature)_feature);
            if (_feature.isTransportFeature) (_feature as TransportFeature).unlink();
            Object.Destroy(_feature.gameObject);
        }
        _feature = null;
    }

    public void confirmFeature()
    {
        _feature.temporary = false;
        renderFeature();
    }

    //Instantiates the GameObject representing this tile
    public void render()
    {
        gameObject = Object.Instantiate(Assets.assets.allsidesPrefab, position, Quaternion.identity);
    }

    public void renderFeature()
    {
        if (_feature != null)
        {
            _feature.render();
        }
    }

    private bool mouseOverMode = false;
    public void setMouseOverBehaviour(bool mouseOverMode)
    {
        if (this.mouseOverMode != mouseOverMode)
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

    public override bool isGroundTile
    {
        get
        {
            return true;
        }
    }
}
