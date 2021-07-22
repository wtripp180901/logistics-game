using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldButton : MonoBehaviour {

    [SerializeField]
    private BUTTON_FUNCTION buttonFunction;
    private float xBounds;

	// Use this for initialization
	void Start () {
        Vector3 bounds = GetComponent<SpriteRenderer>().bounds.extents;
        xBounds = bounds.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(InputAdapter.clickInput == INPUT.UP)
        {
            if((InputAdapter.position - (Vector2)transform.position).magnitude < xBounds)
            {
                ButtonReciever.recieveButtonClick(buttonFunction);
            }
        }
	}
}
