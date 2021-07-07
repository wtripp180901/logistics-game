using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawingManager {

    private static Drawer toDraw = null;

    public static void startDrawing(Drawer featureToDraw)
    {
        toDraw = featureToDraw;
    }

    public static void Update(Map map)
    {
        if (toDraw != null)
        {
            toDraw.Update();
        }
    }

    public static void finishCurrentDrawing()
    {
        toDraw = null;
    }
}
