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
        throw new System.Exception("not implemented");
    }

    

    public static void createStopNumberUI(GameObject asset, TransportHubFeature hub, string text)
    {
        WorldUIStateManager.createStopUI(asset, hub, text);
    }

    public static void removeStopNumberUI(TransportHubFeature hub)
    {
        WorldUIStateManager.removeStopUIAtHub(hub);
    }
}
