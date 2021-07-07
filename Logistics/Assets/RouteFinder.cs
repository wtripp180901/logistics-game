using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RouteFinder {

    private class PfNode
    {
        public int cost;
        public readonly Vector2 position;
        public PfNode parent;
        public TransportFeature nodeOf;

        public PfNode(PfNode parent,TransportFeature nodeOf)
        {
            this.nodeOf = nodeOf;
            this.parent = parent;
            position = nodeOf.parent.position;
            if (parent == null) cost = 1;
            else cost = 1 + parent.cost;
        }
    }

	public static Dictionary<TransportHubFeature,List<Vector2>> getRoutes(TransportHubFeature source)
    {
        //Djikstras
        List<PfNode> closedList = new List<PfNode>();
        List<PfNode> openList = new List<PfNode>();
        PfNode currentNode = new PfNode(null, source);
        closedList.Add(currentNode);
        openList.AddRange(computeAdjacents(currentNode,closedList));

        int kill = 0;
        while(openList.Count > 0 && kill < 1000)
        {
            openList.Sort(sortingFunction);
            currentNode = openList[0];
            closedList.Add(currentNode);
            openList.AddRange(computeAdjacents(currentNode, closedList));
            kill++;
        }
        if (kill == 1000) Debug.Log("kill limit");
        //Finding hubs
        for(int i = closedList.Count - 1;i >= 0; i--)
        {
            if (!closedList[i].nodeOf.isHub) closedList.RemoveAt(i);
        }
        Dictionary<TransportHubFeature, List<Vector2>> routes = new Dictionary<TransportHubFeature, List<Vector2>>();
        for (int i = 0;i < closedList.Count; i++)
        {
            List<Vector2> route = new List<Vector2>();
            PfNode current = closedList[i];
            while(current.nodeOf != source)
            {
                route.Add(current.position);
                current = current.parent;
            }
            route.Reverse();
            routes.Add((TransportHubFeature)closedList[i].nodeOf, route);
        }
        return routes;
    }

    private static List<PfNode> computeAdjacents(PfNode adjsOf, List<PfNode> closedList)
    {
        List<PfNode> adjs = new List<PfNode>();
        for (int i = 0; i < adjsOf.nodeOf.links.Count; i++)
        {
            if (!(adjsOf.parent != null && adjsOf.nodeOf.links[i].parent.position == adjsOf.parent.position))
            {
                PfNode newPfNode = new PfNode(adjsOf, adjsOf.nodeOf.links[i]);
                int indexInClosedList = indexIfInList(newPfNode, closedList);
                if(indexInClosedList == -1) adjs.Add(newPfNode);
                else
                {
                    if(newPfNode.cost < closedList[indexInClosedList].cost)
                    {
                        closedList[indexInClosedList] = newPfNode;
                    }
                }
            }
        }
        return adjs;
    }

    //Returns the index at which the given node appears in the list if it is in the list, returns -1 if not
    private static int indexIfInList(PfNode node,List<PfNode> list)
    {
        for(int i = 0;i < list.Count; i++)
        {
            if (list[i].position == node.position) return i;
        }
        return -1;
    }

    private static int sortingFunction(PfNode x,PfNode y)
    {
        if (x.cost > y.cost) return -1;
        else if (x.cost == y.cost) return 0;
        else return 1;
    }
}
