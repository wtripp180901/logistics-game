using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RouteFinder {

    protected class PfNode
    {
        public int g;
        public int h;
        public int f { get { return h + g; } }
        public readonly Vector2 position;
        public PfNode parent;
        public TransportFeature nodeOf;

        public PfNode(PfNode parent,TransportFeature nodeOf,Vector2 destinationPosition)
        {
            this.nodeOf = nodeOf;
            this.parent = parent;
            position = nodeOf.parent.position;
            if (parent == null) g = 1;
            else g = 1 + parent.g;
            h = (int)(destinationPosition - position).magnitude;
        }
    }

    protected Vector2 destinationPosition;

    public List<Vector2> findPath(TransportHubFeature source,TransportHubFeature destination)
    {
        List<PfNode> openList = new List<PfNode>();
        List<PfNode> closedList = new List<PfNode>();
        destinationPosition = destination.parent.position;

        PfNode currentNode = new PfNode(null, source, destinationPosition);
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
        if (x.f > y.f) return 1;
        else if (x.f == y.f) return 0;
        else return -1;
    }

    protected abstract List<PfNode> computeNewNodes(PfNode computeFrom, List<PfNode> closedList, List<PfNode> openList);

    protected static bool nodesEqual(PfNode x,PfNode y)
    {
        return x.position == y.position;
    }
}
