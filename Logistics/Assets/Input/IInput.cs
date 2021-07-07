using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput {
    INPUT clickInput { get; }
    float scrollInput { get; }
    Vector2 position { get; }
    Vector2Int screenDrag { get; }
}
