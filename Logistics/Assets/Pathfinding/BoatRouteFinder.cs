using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRouteFinder : RouteFinder {

    protected override PfNode[] makeAdjacentPfNodes(PfNode computeFrom)
    {
        Tile[] links = (computeFrom as BoatPfNode).nodeOf.adjacentTiles;
        List<BoatPfNode> nodes = new List<BoatPfNode>();
        for (int i = 0; i < links.Length; i++)
        {
            bool acceptableTile = false;
            if (links[i] != null)
            {
                if (!links[i].isGroundTile) acceptableTile = true;
                else
                {
                    Feature feature = (links[i] as GroundTile).getFeature(false);
                    if (links[i].isGroundTile && feature != null && feature.featureType == FEATURES.SEAPORT) acceptableTile = true;
                }
            }
            if(acceptableTile) nodes.Add(new BoatPfNode(computeFrom, destinationPosition, links[i]));
        }
        return nodes.ToArray();
    }

    protected override PfNode initialNode(TransportHubFeature source)
    {
        return new BoatPfNode(null, destinationPosition, source.parent);
    }
}

