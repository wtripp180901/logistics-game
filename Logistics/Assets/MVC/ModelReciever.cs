using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ModelReciever {

	public static void finishCreationAction()
    {
        UIStateManager.changeState(UI_STATE.NEUTRAL);
    }

    public static void exitCurrentState()
    {
        DrawingManager.reset();
        VehicleCreatorManager.reset();
        ConfirmationManager.reset();
        UIStateManager.exitState();
    }

    public static void createConfirmationMenu(Vector2 position)
    {
        float offset = Assets.assets.tilePrefabWidth / 2;
        WorldUIStateManager.clearConfirmationButtons();
        WorldUIStateManager.createWorldUI(Assets.assets.confirmButtonPrefab, new Vector2(position.x - offset, position.y - offset));
        WorldUIStateManager.createWorldUI(Assets.assets.cancelButtonPrefab, new Vector2(position.x + offset, position.y - offset));
    }

    //public static void clearWorldUI() { WorldUIStateManager.clear(); }

    public static void createStopNumberUI(GameObject asset, TransportHubFeature hub, string text)
    {
        WorldUIStateManager.createStopUI(asset, hub, text);
    }

    public static void removeStopNumberUI(TransportHubFeature hub)
    {
        WorldUIStateManager.removeStopUIAtHub(hub);
    }
}
