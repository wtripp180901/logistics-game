using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionConfirmer : Confirmer {

    private Tile[] toConfirm;

    public ConnectionConfirmer(List<Tile> toConfirm)
    {
        this.toConfirm = toConfirm.ToArray();
    }


    public override void createUI()
    {
        GameObject baseGO = toConfirm[toConfirm.Length - 1].gameObject;
        Vector2 basePos = baseGO.transform.position;
        ModelReciever.createConfirmationMenu(basePos);
    }

    public override void confirm()
    {
        for(int i = 0;i < toConfirm.Length; i++)
        {
            toConfirm[i].confirmFeature();
        }
    }

    public override void cancel()
    {
        FEATURES toRedraw = toConfirm[0].getFeature(true).featureType;
        for (int i = 0; i < toConfirm.Length; i++)
        {
            toConfirm[i].removeFeature();
        }
        DrawingManager.startDrawing(DrawerFactory.build(toRedraw));
    }

    public override void reset()
    {
        for(int i = 0;i < toConfirm.Length; i++)
        {
            toConfirm[i].removeFeature();
        }
    }
}
