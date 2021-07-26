using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfirmationManager {

    private static Confirmer confirmer;

    public static void requestConfirmation(List<Tile> toConfirm)
    {
        confirmer = new ConnectionConfirmer(toConfirm);
        confirmer.createUI();
    }

    public static void requestConfirmation(Vector2 menuPosition,VEHICLE vehicleType)
    {
        confirmer = new StopsConfirmer(menuPosition, vehicleType);
        confirmer.createUI();
    }

    public static void confirm()
    {
        confirmer.confirm();
        finish();
    }

    public static void cancel()
    {
        confirmer.cancel();
        finish();
    }

    private static void finish()
    {
        //ModelReciever.finishCreationAction();
        confirmer = null;
    }
}
