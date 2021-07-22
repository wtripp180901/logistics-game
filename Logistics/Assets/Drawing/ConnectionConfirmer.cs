using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConnectionConfirmer {

    private static Tile[] toConfirm;

    private static GameObject[] buttons;

	public static void requestConfirmation(List<Tile> _toConfirm)
    {
        toConfirm = _toConfirm.ToArray();
        GameObject baseGO = toConfirm[toConfirm.Length - 1].gameObject;
        Vector2 basePos = baseGO.transform.position;
        float offset = baseGO.GetComponent<SpriteRenderer>().bounds.extents.x;
        buttons = new GameObject[2]
        {
            Object.Instantiate(Assets.confirmButtonPrefab, new Vector3(basePos.x - offset,basePos.y - offset,-0.3f),Quaternion.identity),
            Object.Instantiate(Assets.cancelButtonPrefab, new Vector3(basePos.x + offset,basePos.y - offset,-0.3f),Quaternion.identity)
        };
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
        Object.Destroy(buttons[0]);
        Object.Destroy(buttons[1]);
        buttons = null;
        toConfirm = null;
    }
}
