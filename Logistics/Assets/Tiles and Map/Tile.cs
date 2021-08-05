using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile {

    public readonly Vector2 position;

    public Tile(float x,float y)
    {
        position = new Vector2(x, y);
    }

    public Tile[] adjacentTiles = new Tile[4] { null, null, null, null };
    public Tile left { get { return adjacentTiles[0]; } set { adjacentTiles[0] = value;} }
    public Tile right { get { return adjacentTiles[1]; } set { adjacentTiles[1] = value; } }
    public Tile up { get { return adjacentTiles[2]; } set { adjacentTiles[2] = value; } }
    public Tile down { get { return adjacentTiles[3]; } set { adjacentTiles[3] = value; } }

    public bool adjacentTo(Tile tile)
    {
        for(int i = 0;i < adjacentTiles.Length; i++)
        {
            if (adjacentTiles[i] == tile) return true;
        }
        return false;
    }

    //Returns true if point is inside tile's sprite
    private static float bounds = Assets.assets.tilePrefabWidth / 2;
    public bool pointInside(Vector2 pos)
    {
        return
            pos.x <= position.x + bounds &&
            pos.x >= position.x - bounds &&
            pos.y <= position.y + bounds &&
            pos.y >= position.y - bounds;
    }

    public abstract bool isGroundTile { get; }
}
