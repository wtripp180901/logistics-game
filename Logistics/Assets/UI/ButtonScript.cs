using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonScript : MonoBehaviour {

    private Button button;
    private ScriptableObject scriptableObject;
    /*[SerializeField]
    private Sprite icon;*/

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonAction);
        //gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = icon;
	}

    protected abstract void buttonAction();
}
