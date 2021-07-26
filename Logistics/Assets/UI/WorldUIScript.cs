using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIScript : MonoBehaviour {

    public Vector3 worldPosition;

    void Start()
    {
        transform.position = worldPosition;
    }

    // Update is called once per frame
    void Update () {
        transform.position = worldPosition;
	}
}
