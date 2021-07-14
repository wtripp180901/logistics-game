using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RouteFinder {

    private class PfNode
    {
        public int g;
        public int h;
        public int f { get { return h + g; } }
        public readonly Vector2 position;
        public PfNode parent;
        public TransportFeature nodeOf;

        public PfNode(PfNode parent,TransportFeature nodeOf)
        {
            this.nodeOf = nodeOf;
            this.parent = parent;
            position = nodeOf.parent.position;
            if (parent == null) g = 1;
            else g = 1 + parent.g;
            h = (int)(destinationPosition - position).magnitude;
        }
    }

    private static Vector2 destinationPosition;

    public static List<Vector2> findPath(TransportHubFeature source,TransportHubFeature destination)
    {
        List<PfNode> openList = new List<PfNode>();
        List<PfNode> closedList = new List<PfNode>();
        destinationPosition = destination.parent.position;

        PfNode currentNode = new PfNode(null, source);
        closedList.Add(currentNode);
        openList.AddRange(computeNewNodes(closedList[0], closedList, openList));

        while(openList.Count > 0)
        {
            openList.Sort(sortingFunction);
            currentNode = openList[0];
            if (currentNode.position == destinationPosition) break;
            openList.RemoveAt(0);
            closedList.Add(currentNode);
            openList.AddRange(computeNewNodes(currentNode, closedList, openList));
        }

        if (currentNode.position != destinationPosition) return null;
        else
        {
            List<Vector2> path = new List<Vector2>();
            while (currentNode != null)
            {
                path.Add(currentNode.position);
                currentNode = currentNode.parent;
            }
            path.Add(source.parent.position);
            path.Reverse();
            return path;
        }

    }

    private static int sortingFunction(PfNode x,PfNode y)
    {
        if (x.f > y.f) return -1;
        else if (x.f == y.f) return 0;
        else return 1;
    }

    private static List<PfNode> computeNewNodes(PfNode computeFrom,List<PfNode> closedList,List<PfNode> openList)
    {
        List<PfNode> adjacents = new List<PfNode>();
        for(int i = 0;i < computeFrom.nodeOf.links.Count; i++)
        {
            PfNode currentNode = new PfNode(computeFrom, computeFrom.nodeOf.links[i]);
            bool alreadyInList = false;
            for (int j = 0;j < closedList.Count; j++)
            {
                if (nodesEqual(closedList[j], currentNode)) alreadyInList = true;
            }
            for(int j = 0;j < openList.Count; j++)
            {
                if(nodesEqual(openList[j],currentNode) && currentNode.g < openList[j].g)
                {
                    alreadyInList = true;
                    openList[j] = currentNode;
                }
            }
            if (!alreadyInList) adjacents.Add(currentNode);
        }
        return adjacents;
    }

    private static bool nodesEqual(PfNode x,PfNode y)
    {
        return x.position == y.position;
    }
}
