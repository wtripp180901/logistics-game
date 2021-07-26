using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConnectionConfirmer {

    private static Tile[] toConfirm;

	public static void requestConfirmation(List<Tile> _toConfirm)
    {
        toConfirm = _toConfirm.ToArray();
        GameObject baseGO = toConfirm[toConfirm.Length - 1].gameObject;
        Vector2 basePos = baseGO.transform.position;
        ModelReciever.createConfirmationMenu(basePos);
    }

    public static void confirm()
    {
        for(int i = 0;i < toConfirm.Length; i++)
        {
            toConfirm[i].confirmFeature();
        }
        finish();
    }

    public static void cancel()
    {
        FEATURES toRedraw = toConfirm[0].getFeature(true).featureType;
        for (int i = 0; i < toConfirm.Length; i++)
        {
            toConfirm[i].removeFeature();
        }
        DrawingManager.startDrawing(DrawerFactory.build(toRedraw));
        finish();
    }

    private static void finish()
    {
        ModelReciever.finishCreationAction();
        toConfirm = null;
    }
}
