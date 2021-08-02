using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUTTON_FUNCTION { OPEN_TRANSPORT_MENU, OPEN_VEHICLE_MENU,CANCEL,CONFIRM, TOGGLE_PAUSE_PLAY, DOUBLE_SPEED }

public static class ButtonReciever {

    private static Dictionary<BUTTON_FUNCTION, UI_STATE> stateChangingUIDictionary = new Dictionary<BUTTON_FUNCTION, UI_STATE>()
    {
        { BUTTON_FUNCTION.OPEN_TRANSPORT_MENU,UI_STATE.TRANSPORT_MENU },
        { BUTTON_FUNCTION.OPEN_VEHICLE_MENU,UI_STATE.VEHICLE_MENU }
    };

	public static void recieveButtonClick(BUTTON_FUNCTION function)
    {
        switch (function)
        {
            case BUTTON_FUNCTION.CONFIRM:
                ConfirmationManager.confirm();
                UIStateManager.changeState(UI_STATE.NEUTRAL);
                break;
            case BUTTON_FUNCTION.CANCEL:
                ConfirmationManager.cancel();
                WorldUIStateManager.clear();
                break;
            case BUTTON_FUNCTION.TOGGLE_PAUSE_PLAY:
                TimeManager.togglePausePlay();
                break;
            case BUTTON_FUNCTION.DOUBLE_SPEED:
                TimeManager.toggleDoubleSpeed();
                break;
            default:
                UI_STATE nextState;
                if (stateChangingUIDictionary.TryGetValue(function, out nextState)) UIStateManager.changeState(nextState);
                else throw new System.Exception("Trying to use state changing dictionary for incompatible button");
                break;
        }
    }

    public static void recieveButtonClick(VEHICLE vehicle)
    {
        UIStateManager.changeState(UI_STATE.BLANK);
        VehicleCreatorManager.startCreation(VehicleCreatorFactory.build(vehicle));
    }

    public static void recieveButtonClick(FEATURES feature)
    {
        UIStateManager.changeState(UI_STATE.BLANK);
        DrawingManager.startDrawing(DrawerFactory.build(feature));
    }
}
