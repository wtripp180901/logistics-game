using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateValidator {

    private int width;
    private int height;
    private int borderWidth;
    //private List<Vector2Int> closedList = new List<Vector2Int>();

    public CoordinateValidator(int width,int height,int borderWidth)
    {
        this.width = width;
        this.height = height;
        this.borderWidth = borderWidth;
    }

    public bool valid(int x,int y)
    {
        return x >= borderWidth && x < width - borderWidth && y >= borderWidth && y < height - borderWidth;
    }

    /*private bool inClosedList(int x,int y)
    {
        for(int i = 0;i < closedList.Count; i++)
        {
            if (closedList[i].x == x && closedList[i].y == y) return true;
        }
        return false;
    }

    public void ban(Vector2Int coordsToBan){ closedList.Add(coordsToBan); }
    public void ban(Vector2Int[] coordsToBan) { closedList.AddRange(coordsToBan); }*/
}
