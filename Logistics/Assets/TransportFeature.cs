using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportFeature : Feature
{
    public bool fullyLinked = false;
    protected int linkCapacity;
    public List<TransportFeature> links = new List<TransportFeature>();
    public override bool isTransportFeature { get { return true; } }

    public TransportFeature(FEATURES featureType, bool temporary, Tile parent) : base(featureType, temporary, parent)
    {
        if (featureType == FEATURES.RAIL) linkCapacity = 2;
        else linkCapacity = 4;
    }

    //Tries to link with all adjacent features
    public void link()
    {
        Feature[] adjFeats = adjacentLinkableFeatures();
        for (int i = 0;i < adjFeats.Length; i++)
        {
            tryLinkTo((TransportFeature)adjFeats[i]);
        }
    }

    //Links this TransportFeature with linkTo if both have available links
    protected virtual void tryLinkTo(TransportFeature linkTo)
    {
        if (linkTo.links.Count < linkTo.linkCapacity && links.Count < linkCapacity)
        {
            links.Add(linkTo);
            linkTo.links.Add(this);
        }
    }

    //Gets adjacent features of same type and hubs that allow this feature
    private Feature[] adjacentLinkableFeatures()
    {
        List<Feature> adjFeats = new List<Feature>();
        for (int i = 0; i < parent.adjacentTiles.Length; i++)
        {
            if (parent.adjacentTiles[i] != null)
            {
                Feature feat = parent.adjacentTiles[i].getFeature(temporary);
                TransportFeature transportFeature;
                if(feat != null && feat.isTransportFeature) {
                    transportFeature = (TransportFeature)feat;
                    if(transportFeature.canLinkWith(featureType)) adjFeats.Add(feat);
                }
            }
                    
        }
        return adjFeats.ToArray();
    }

    public virtual bool canLinkWith(FEATURES feat)
    {
        return feat == featureType;
    }

}
    

