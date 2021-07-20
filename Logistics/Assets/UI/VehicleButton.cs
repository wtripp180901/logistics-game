using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleButton : ButtonScript {

    [SerializeField]
    private VEHICLE vehicle;

    protected override void buttonAction()
    {
        ButtonReciever.recieveButtonClick(vehicle);
    }
}
