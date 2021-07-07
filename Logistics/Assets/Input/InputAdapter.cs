using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum INPUT { DOWN, UP, NEUTRAL, HELD };

public static class InputAdapter {

    
    private static readonly IInput input = new PCInput();

    public static INPUT clickInput { get { return input.clickInput; } }
    public static float scrollInput { get { return input.scrollInput; } }
    public static Vector2 position { get { return input.position; } }
    public static Vector2Int screenDrag { get { return input.screenDrag; } }
}
