using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopsConfirmer : Confirmer {

    private Vector2 menuPosition;
    private VEHICLE vehicleType;

    public StopsConfirmer(Vector2 menuPosition,VEHICLE vehicleType)
    {
        this.menuPosition = menuPosition;
        this.vehicleType = vehicleType;
    }

    public override void createUI()
    {
        ModelReciever.createConfirmationMenu(menuPosition);
    }

    public override void confirm()
    {
        VehicleCreatorManager.finish();
    }

    public override void cancel()
    {
        VehicleCreatorManager.startCreation(VehicleCreatorFactory.build(vehicleType));
    }

    public override void reset()
    {
        
    }
}
