using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPfNode : PfNode {

    public TransportFeature nodeOf;

    public GroundPfNode(PfNode parent, Vector2 destinationPosition, TransportFeature nodeOf) : base(parent, destinationPosition)
    {
        this.nodeOf = nodeOf;
        setPosition();
    }

    protected override void setPosition()
    {
        position = nodeOf.parent.position;
    }
}
