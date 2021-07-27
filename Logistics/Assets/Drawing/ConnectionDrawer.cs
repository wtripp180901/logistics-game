using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionDrawer : Drawer {

    private bool startedLine = false;
    private List<Tile> line = new List<Tile>();

    public ConnectionDrawer(Map map,FEATURES toDraw) : base(map,toDraw){}

    protected override void concreteUpdate()
    {
        if (startedLine)
        {
            if (InputAdapter.clickInput == INPUT.HELD)
            {
                if (mouseNotInLine())
                {
                    Tile mouseTile = map.tileWithMouseInside();
                    if (mouseTile != null && (line.Count == 0 || line[line.Count - 1].adjacentTo(mouseTile)))
                    {
                        line.Add(mouseTile);
                        mouseTile.addFeature(toDraw, true);
                    }
                }
            }
        }
        else
        {
            if (InputAdapter.clickInput == INPUT.DOWN)
            {
                startedLine = true;
            }
        }
    }

    public override void reset()
    {
        for(int i = 0;i < line.Count; i++)
        {
            line[i].removeFeature();
        }
    }

    private bool mouseNotInLine()
    {
        for (int i = 0; i < line.Count; i++)
        {
            if (line[i].pointInside(InputAdapter.position)) return false;
        }
        return true;
    }

    protected override void finishDrawing()
    {
        /*for (int i = 0; i < line.Count; i++)
        {
            line[i].confirmFeature();
        }*/
        ConfirmationManager.requestConfirmation(line);
        toDraw = FEATURES.NONE;
        startedLine = false;
        line.Clear();
    }
}
