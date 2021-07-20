using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BUTTON_FUNCTION { OPEN_TRANSPORT_MENU, OPEN_VEHICLE_MENU }

public abstract class ButtonScript : MonoBehaviour {

    private Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonAction);
	}

    protected abstract void buttonAction();
}
