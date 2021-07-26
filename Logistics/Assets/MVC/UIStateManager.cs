using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI_STATE { BLANK, NEUTRAL, VEHICLE_MENU,TRANSPORT_MENU}

public static class UIStateManager {

    private static List<GameObject> currentUI = new List<GameObject>();
    private static UI_STATE _state = UI_STATE.BLANK; public static UI_STATE state { get { return _state; } }

    private static Dictionary<UI_STATE, GameObject[]> objectsToRenderDictionary = new Dictionary<UI_STATE, GameObject[]>()
    {
        { UI_STATE.NEUTRAL, new GameObject[] {Assets.openTransportButtonPrefab,Assets.openVehicleButtonPrefab} },
        { UI_STATE.BLANK, new GameObject[0] },
        { UI_STATE.VEHICLE_MENU, new GameObject[] {Assets.vehicleBarPrefab} },
        { UI_STATE.TRANSPORT_MENU, new GameObject[] {Assets.transportBarPrefab} }
    };

	public static void changeState(UI_STATE state)
    {
        destroyCurrent();
        _state = state;
        GameObject[] toCreate;
        if (objectsToRenderDictionary.TryGetValue(_state, out toCreate)) addAndRenderObjects(toCreate);
        else throw new System.Exception("State not implemented in dictionary "+state);
    }

    private static void addAndRenderObjects(GameObject[] toCreate)
    {
        for (int i = 0; i < toCreate.Length; i++)
        {
            currentUI.Add(Object.Instantiate(toCreate[i], Assets.canvas));
        }
    }

    private static void destroyCurrent()
    {
        for(int i = currentUI.Count - 1;i >= 0; i--)
        {
            Object.Destroy(currentUI[i]);
            currentUI.RemoveAt(i);
        }
        WorldUIStateManager.clear();
    }
}
