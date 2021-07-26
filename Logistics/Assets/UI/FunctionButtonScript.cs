using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButtonScript : ButtonScript {

    [SerializeField]
    private BUTTON_FUNCTION buttonFunction;

    protected override void buttonAction()
    {
        ButtonReciever.recieveButtonClick(buttonFunction);
    }
}
