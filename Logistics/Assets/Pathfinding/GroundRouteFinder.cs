using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRouteFinder : RouteFinder {

    protected override List<PfNode> computeNewNodes(PfNode computeFrom, List<PfNode> closedList, List<PfNode> openList)
    {
        List<PfNode> adjacents = new List<PfNode>();
        for (int i = 0; i < computeFrom.nodeOf.getLinks.Length; i++)
        {
            PfNode currentNode = new PfNode(computeFrom, computeFrom.nodeOf.getLinks[i], destinationPosition);
            bool alreadyInList = false;
            for (int j = 0; j < closedList.Count; j++)
            {
                if (nodesEqual(closedList[j], currentNode)) alreadyInList = true;
            }
            for (int j = 0; j < openList.Count; j++)
            {
                if (nodesEqual(openList[j], currentNode) && currentNode.g < openList[j].g)
                {
                    alreadyInList = true;
                    openList[j] = currentNode;
                }
            }
            if (!alreadyInList) adjacents.Add(currentNode);
        }
        return adjacents;
    }
}
