using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfirmationManager {

    private static Confirmer confirmer;

    public static void requestConfirmation(List<GroundTile> toConfirm)
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

    public static void reset()
    {
        if(confirmer != null) confirmer.reset();
        finish();
    }

    private static void finish()
    {
        //ModelReciever.finishCreationAction();
        confirmer = null;
    }
}
