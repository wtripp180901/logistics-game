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
        Debug.Log("exit");
    }

    public static void createConfirmationMenu(Vector2 position)
    {
        float offset = Assets.tilePrefabWidth / 2;
        WorldUIStateManager.clearConfirmationButtons();
        WorldUIStateManager.createWorldUI(Assets.confirmButtonPrefab, new Vector2(position.x - offset, position.y - offset));
        WorldUIStateManager.createWorldUI(Assets.cancelButtonPrefab, new Vector2(position.x + offset, position.y - offset));
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
