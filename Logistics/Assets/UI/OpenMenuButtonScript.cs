using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuButtonScript : ButtonScript {

    [SerializeField]
    private BUTTON_FUNCTION buttonFunction;

    protected override void buttonAction()
    {
        ButtonReciever.recieveButtonClick(buttonFunction);
    }
}
