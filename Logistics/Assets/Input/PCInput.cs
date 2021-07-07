using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : IInput {

	public INPUT clickInput
    {
        get
        {
            if (Input.GetMouseButtonDown(0)) return INPUT.DOWN;
            else if (Input.GetMouseButton(0)) return INPUT.HELD;
            else if (Input.GetMouseButtonUp(0)) return INPUT.UP;
            return INPUT.NEUTRAL;
        }
    }
    public float scrollInput
    {
        get
        {
            return Input.mouseScrollDelta.y;
        }
    }
    public Vector2 position
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
        }
    }
    public Vector2Int screenDrag
    {
        get
        {
            const float screenLimit = 0.05f;
            int x = 0;
            int y = 0;
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            if (Input.mousePosition.x < screenWidth * screenLimit) x = -1;
            if (Input.mousePosition.x > screenWidth - (screenWidth * screenLimit)) x = 1;
            if (Input.mousePosition.y < screenHeight * screenLimit) y = -1;
            if (Input.mousePosition.y > screenHeight - (screenHeight * screenLimit)) y = 1;
            return new Vector2Int(x, y);
        }
    }
}
