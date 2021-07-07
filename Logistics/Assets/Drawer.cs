using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Drawer {

    protected Map map;
    protected FEATURES toDraw;

    public Drawer(Map map,FEATURES toDraw)
    {
        this.map = map;
        this.toDraw = toDraw;
    }

    public void Update()
    {
        concreteUpdate();
        if(InputAdapter.clickInput == INPUT.UP)
        {
            finishDrawing();
            DrawingManager.finishCurrentDrawing();
        }
    }

    protected abstract void concreteUpdate();
    protected abstract void finishDrawing();
}
