using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRouteFinder : RouteFinder {

    protected override PfNode[] makeAdjacentPfNodes(PfNode computeFrom)
    {
        TransportFeature[] links = computeFrom.nodeOf.getLinks;
        List<PfNode> nodes = new List<PfNode>();
        for (int i = 0; i < links.Length; i++)
        {
            nodes.Add(new PfNode(computeFrom, computeFrom.nodeOf.getLinks[i], destinationPosition));
        }
        return nodes.ToArray();
    }
}
