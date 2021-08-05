using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : Tile {

    public WaterTile(float x, float y) : base(x, y) { }

    public override bool isGroundTile
    {
        get
        {
            return false;
        }
    }
}
