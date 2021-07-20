using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureButton : ButtonScript {

    [SerializeField]
    private FEATURES feature;

    protected override void buttonAction()
    {
        ButtonReciever.recieveButtonClick(feature);
    }
}
