using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDrawer : Drawer {

    public HubDrawer(Map map, FEATURES toDraw) : base(map, toDraw) { }

    private Tile prevTile = null;
    private Tile currentTile = null;

    protected override void concreteUpdate()
    {
        currentTile = map.tileWithMouseInside();
        if(currentTile != null) currentTile.setMouseOverBehaviour(true);
        if (currentTile != prevTile)
        {
            if(prevTile != null) prevTile.setMouseOverBehaviour(false);
            prevTile = currentTile;
        }
    }

    protected override void finishDrawing()
    {
        if (currentTile != null)
        {
            currentTile.addFeature(toDraw, false);
            currentTile.setMouseOverBehaviour(false);
        }
    }
}
