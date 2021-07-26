using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawingManager {

    private static Drawer toDraw = null;
    private static Drawer toDrawNextFrame = null;
    private static bool drawingMode = false;

    public static void startDrawing(Drawer featureToDraw)
    {
        drawingMode = true;
        toDrawNextFrame = featureToDraw;
    }

    public static void Update(Map map)
    {
        if (drawingMode)
        {
            if(toDraw != null) toDraw.Update();
            else
            {
                toDraw = toDrawNextFrame;
                toDrawNextFrame = null;
            }
        }
    }

    public static void finishCurrentDrawing()
    {
        toDraw = null;
        drawingMode = false;
    }
}
