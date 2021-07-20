using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModelReciever {

	public static void finishCreationAction()
    {
        UIStateManager.changeState(UI_STATE.NEUTRAL);
    }
}
