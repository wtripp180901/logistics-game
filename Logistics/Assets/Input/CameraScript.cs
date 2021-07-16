using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private Camera cam;
    const float cameraSpeed = 2.5f;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        cam.orthographicSize -= InputAdapter.scrollInput;
        Vector2Int screenDrag = InputAdapter.screenDrag;
        transform.position += new Vector3(screenDrag.x,screenDrag.y,0) * cameraSpeed * Time.deltaTime;
    }
}
