using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRouteFinder : RouteFinder {

    protected override PfNode[] makeAdjacentPfNodes(PfNode computeFrom)
    {
        TransportFeature[] links = (computeFrom as GroundPfNode).nodeOf.getLinks;
        List<PfNode> nodes = new List<PfNode>();
        for (int i = 0; i < links.Length; i++)
        {
            nodes.Add(new GroundPfNode(computeFrom, destinationPosition, links[i]));
        }
        return nodes.ToArray();
    }

    protected override PfNode initialNode(TransportHubFeature source)
    {
        return new GroundPfNode(null, destinationPosition,source);
    }
}
