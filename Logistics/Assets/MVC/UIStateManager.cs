using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI_STATE { BLANK, NEUTRAL, VEHICLE_MENU,TRANSPORT_MENU,MENU}

public static class UIStateManager {

    private static List<GameObject> currentUI = new List<GameObject>();
    private static UI_STATE _state = UI_STATE.BLANK; //public static UI_STATE state { get { return _state; } }
    private static UI_STATE prevState = UI_STATE.BLANK;

    private static readonly Dictionary<UI_STATE, UI_STATE> parentStateOf = new Dictionary<UI_STATE, UI_STATE>()
    {
        {UI_STATE.NEUTRAL,UI_STATE.MENU },
        {UI_STATE.VEHICLE_MENU,UI_STATE.NEUTRAL },
        {UI_STATE.TRANSPORT_MENU,UI_STATE.NEUTRAL },
        {UI_STATE.MENU,UI_STATE.NEUTRAL }
    };

    private static Dictionary<UI_STATE, GameObject[]> objectsToRenderDictionary = new Dictionary<UI_STATE, GameObject[]>()
    {
        { UI_STATE.NEUTRAL, new GameObject[] {Assets.assets.openTransportButtonPrefab,Assets.assets.openVehicleButtonPrefab } },
        { UI_STATE.BLANK, new GameObject[0] },
        { UI_STATE.VEHICLE_MENU, new GameObject[] {Assets.assets.vehicleBarPrefab } },
        { UI_STATE.TRANSPORT_MENU, new GameObject[] {Assets.assets.transportBarPrefab } }
    };

	public static void changeState(UI_STATE state)
    {
        destroyCurrent();
        prevState = _state;
        _state = state;
        GameObject[] toCreate;
        if (objectsToRenderDictionary.TryGetValue(_state, out toCreate)) addAndRenderObjects(toCreate);
        else throw new System.Exception("State not implemented in dictionary "+state);
    }

    public static void exitState()
    {
        UI_STATE parentState;
        if(parentStateOf.TryGetValue(_state,out parentState))
        {
            changeState(parentState);
        }
        else
        {
            changeState(prevState);
        }
    }

    private static void addAndRenderObjects(GameObject[] toCreate)
    {
        for (int i = 0; i < toCreate.Length; i++)
        {
            currentUI.Add(Object.Instantiate(toCreate[i], Assets.assets.canvas));
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
