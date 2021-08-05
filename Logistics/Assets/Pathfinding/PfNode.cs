using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PfNode {

    public int g;
    public int h;
    public int f { get { return h + g; } }
    public Vector2 position;
    public PfNode parent;
    
    public PfNode(PfNode parent, Vector2 destinationPosition)
    {
        this.parent = parent;
        if (parent == null) g = 1;
        else g = 1 + parent.g;
        h = (int)(destinationPosition - position).magnitude;
    }

    //Must be called in constructor of subclasses
    protected abstract void setPosition();
}
