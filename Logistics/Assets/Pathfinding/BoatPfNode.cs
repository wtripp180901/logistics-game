using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPfNode : PfNode {

    public readonly Tile nodeOf;

    public BoatPfNode(PfNode parent, Vector2 destinationPosition, Tile nodeOf) : base(parent, destinationPosition)
    {
        this.nodeOf = nodeOf;
        setPosition();
    }

    protected override void setPosition()
    {
        position = nodeOf.position;
    }
}
